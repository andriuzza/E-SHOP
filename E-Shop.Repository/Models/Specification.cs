using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Contract.Dto;
namespace E_Shop.Repository.Models
{
    public class Specification
    {
        [ForeignKey("Product")]
        public int Id { get; set; }

        public int Storage { get; set; }
        public SystemType OSType { get; set; }

        public byte Camera { get; set; }

        public Product Product { get; set; }
    }
}
