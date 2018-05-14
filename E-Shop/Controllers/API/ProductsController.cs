using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using E_Shop.Contract.Dto;
using E_Shop.Contract.Helpers;
using E_Shop.Contract.Interfaces;
using Newtonsoft.Json;

namespace E_Shop.Controllers.API
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("post")]
        [HttpPost]
        public IHttpActionResult AddJson()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                 "products.json");

            using (var r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<ProductDto>>(json);

                if ( _productService.AddRangeTaxes(items))
                {
                    return Ok("Successfuly added");
                }

                return BadRequest("Something get wrong");
            }
        }

        [Route("getAll")]
        [HttpPost]
        public IHttpActionResult Get(SearchFilter filter, int pageNumber = 1, int pageSize = 2,
            string searchInput = null)
        {

            var queryString = QueryParameters.Create(pageNumber, pageSize, searchInput);

            var productsList =  _productService.GetAllAsync(filter, queryString);

            if (productsList == null)
            {
                return BadRequest();
            }

            return Ok(productsList);
        }
    }
}
