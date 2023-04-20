using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackageService.Models;

namespace PackageService.Infrastructure.Configuration
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasKey(x => x.PackageId); //Podesavam primarni kljuc tabele
            builder.Property(x => x.PackageId).ValueGeneratedOnAdd();
            builder.Property(x => x.PackageStatus).IsRequired();
            builder.Property(x => x.PackageCode).IsRequired();

        }
    }
}
