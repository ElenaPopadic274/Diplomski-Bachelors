using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Infrastructure;
using EmailService.Interfaces;
using EmailService.Services;
using EmailService.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace EmailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string _cors = "cors";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmailService", Version = "v1" });
                //Ovo dodajemo kako bi mogli da unesemo token u swagger prilikom testiranja
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            //Dodajemo semu autentifikacije i podesavamo da se radi o JWT beareru
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters //Podesavamo parametre za validaciju pristiglih tokena
               {
                   ValidateIssuer = true, //Validira izdavaoca tokena
                   ValidateAudience = false, //Kazemo da ne validira primaoce tokena
                   ValidateLifetime = true,//Validira trajanje tokena
                   ValidateIssuerSigningKey = true, //validira potpis token, ovo je jako vazno!
                   ValidIssuer = Configuration["tokenAddress"], //odredjujemo koji server je validni izdavalac
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))//navodimo privatni kljuc kojim su potpisani nasi tokeni
               };
           });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasToken", policy => policy.RequireClaim("username"));
            });
            //------------SERVICES------------
            services.AddScoped<IMailService, MailService>();

            //------------DATABASE------------
            //registracija db contexta u kontejneru zavisnosti, njegov zivotni vek je Scoped
            services.AddDbContext<MailDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MailDatabase"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33.0-mysql")));
            //Registracija mapera u kontejneru, zivotni vek singleton
            //------------MAPPER------------
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            //------------CORS------------
            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder =>
                {
                    builder.WithOrigins(Configuration["GatewayAddress"], Configuration["PackageServiceAddress"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
        }
      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailService v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors(_cors);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
