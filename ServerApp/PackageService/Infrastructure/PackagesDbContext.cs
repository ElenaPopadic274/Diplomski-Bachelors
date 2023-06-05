using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackageService.Models;

namespace PackageService.Infrastructure
{
    public class PackagesDbContext : DbContext
    {
        public DbSet<Package> Packages { get; set; }
        public PackagesDbContext(DbContextOptions options) : base(options)
        {
        }
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PackagesDbContext).Assembly);
        }
    }
}
