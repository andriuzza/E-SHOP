using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Contract.Helpers;

namespace E_Shop.Contract.Dto
{
    public class PagedList
    {
        public IEnumerable<ProductDto> Products { get; set; }

        public QueryParameters Parameters { get; set; }
    }
}
