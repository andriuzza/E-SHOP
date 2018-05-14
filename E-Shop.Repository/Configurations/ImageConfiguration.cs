
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Repository.Models;

namespace E_Shop.Repository.Configurations
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
            HasKey(x => x.Id);

            Property(c => c.Large)
                .IsRequired();

            Property(c => c.Small)
                .IsRequired();

        }
    }
}
