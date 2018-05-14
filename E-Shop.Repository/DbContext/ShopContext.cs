using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Repository.Configurations;
using E_Shop.Repository.Models;

namespace E_Shop.Repository.DbContext
{
    public class ShopContext : System.Data.Entity.DbContext
    {

        public ShopContext() : base("ShopContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Specification> Specifications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new ManufacturerConfiguration());
            modelBuilder.Configurations.Add(new SpecificationConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
