using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Repository.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Collection<Product> Products { get; set; }
    }
}
