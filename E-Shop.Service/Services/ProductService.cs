using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using E_Shop.Contract.Dto;
using E_Shop.Contract.Helpers;
using E_Shop.Contract.Interfaces;

namespace E_Shop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool AddRangeTaxes(List<ProductDto> items)
        {
            return  _productRepository.AddRange(items);
        }
        
        public PagedList GetAllAsync(SearchFilter filter, QueryParameters parameters)
        {   
            return _productRepository.GetAllAsync(filter, parameters);
        }
    }
}
