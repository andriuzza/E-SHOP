using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Repository.Models;

namespace E_Shop.Repository.Configurations
{
    public class SpecificationConfiguration : EntityTypeConfiguration<Specification>
    {
        public SpecificationConfiguration()
        {
            HasKey(m => m.Id);

            Property(c => c.Camera)
                .IsRequired();

            Property(c => c.Storage)
                .IsRequired();

            Property(c => c.OSType)
                .IsRequired();

        }
    }
}
