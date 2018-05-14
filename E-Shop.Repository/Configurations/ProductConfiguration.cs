using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Repository.Models;

namespace E_Shop.Repository.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(c => c.Price)
                .IsRequired();

            HasRequired(s => s.Image)
            .WithRequiredPrincipal(ad => ad.Product);


            HasRequired(s => s.Specification)
                .WithRequiredPrincipal(ad => ad.Product);

            HasRequired(s => s.Manufacturer)
                .WithMany(g => g.Products)
                .HasForeignKey<int>(s => s.ManufacturerId);

        }
    }
}
