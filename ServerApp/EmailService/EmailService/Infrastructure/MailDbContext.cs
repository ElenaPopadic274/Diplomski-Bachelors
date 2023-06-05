using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Models;

namespace EmailService.Infrastructure
{
    public class MailDbContext : DbContext
    {
        public DbSet<Mail> Emails { get; set; }
        public MailDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MailDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = "server=database-2.c5oonzgvgcgu.eu-north-1.rds.amazonaws.com;uid=admin;pwd=elenabaza;database=elenabaza;"; // Configuration.GetConnectionString("UserDatabase");

            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
