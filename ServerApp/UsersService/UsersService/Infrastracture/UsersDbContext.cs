using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Models;

namespace UsersService.Infrastracture
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected readonly IConfiguration Configuration;

        //public UsersDbContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to mysql with connection string from app settings
        //    var connectionString = "server=database-2.c5oonzgvgcgu.eu-north-1.rds.amazonaws.com;uid=admin;pwd=elenabaza;database=elenabaza;"; // Configuration.GetConnectionString("UserDatabase");
        //    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
        }


    }
}
