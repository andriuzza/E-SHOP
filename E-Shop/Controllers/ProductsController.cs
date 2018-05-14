using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_Shop.Contract.Interfaces;

namespace E_Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Show()
        {
            return View();
        }
    }
}