using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Contract.Dto;
using E_Shop.Contract.Helpers;

namespace E_Shop.Contract.Interfaces
{
    public interface IProductRepository
    {
        bool AddRange(List<ProductDto> items);
        PagedList GetAllAsync(SearchFilter filter, QueryParameters parameters);
    }
}
