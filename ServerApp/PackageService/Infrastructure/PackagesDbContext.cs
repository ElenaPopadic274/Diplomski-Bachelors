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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PackagesDbContext).Assembly);
        }
    }
}
