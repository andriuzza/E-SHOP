using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Repository.Models;

namespace E_Shop.Repository.Configurations
{
    public class ManufacturerConfiguration : EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerConfiguration()
        {
            HasKey(x => x.Id);

            Property(x=>x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
