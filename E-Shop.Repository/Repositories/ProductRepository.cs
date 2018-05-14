using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Shop.Contract.Dto;
using E_Shop.Contract.Interfaces;
using E_Shop.Repository.DbContext;
using E_Shop.Repository.Models;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Mapping;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using E_Shop.Contract.Helpers;

namespace E_Shop.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _dbContext;

        public ProductRepository(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddRange(List<ProductDto> items)
        {
            var Manufacturers = new List<Manufacturer>();
            var products = new List<Product>();

            foreach (var product in items)
            {
                var manufacturerId =  AddManufacturer(product.Specification.Manufacturer);
               

                var productAdd = new Product()
                {
                    Price = product.Price,
                    Name = product.Name,
                    ManufacturerId = manufacturerId
                };
                _dbContext.Products.Add(productAdd);
                _dbContext.SaveChanges();

                var imageId = AddImage(product.Image, productAdd.Id);
                var specificationId = AddSpecification(product.Specification,
                    manufacturerId, product.Specification.OSType, productAdd.Id);
            }

            var isDone =  _dbContext.SaveChanges();
            return isDone != 0;
        }

        public PagedList GetAllAsync(SearchFilter filter, QueryParameters parameters)
        {
            var query = _dbContext.Products
                .IncludeData() // same there
                .ByName(parameters.SearchField); // extension method
          
            var productsQuery = Filtering(filter, query);

            parameters.TotalCount = productsQuery.Count();
            var products =  CheckForPaging(parameters, productsQuery);
            return new PagedList()
            {
                Products = SelectToDtoNot(products),
                Parameters = parameters
            };
        }

        private IQueryable<Product> CheckForPaging(QueryParameters parameters, IQueryable<Product> query)
        {
            if (parameters.PageSize > 20)
            {
                parameters.PageSize = 3;
            }

            return query.OrderBy(x=>x.Name)
                .Skip((parameters.Page - 1) * parameters.PageSize)
                .Take(parameters.PageSize);
        }

        private  IQueryable<Product> Filtering(SearchFilter filter, IQueryable<Product> query)
        {
            var result = query;
            
            if (filter.Storages.Any())
            {
                result = from product2 in result
                    join storage in filter.Storages
                        on product2.Specification.Storage equals storage
                    select product2;
            }

            if (filter.Manufacturers.Any())
            {
                result = from product1 in result
                    join Manufacturer in filter.Manufacturers
                        on  product1.Manufacturer.Name.ToLower() equals Manufacturer
                         select product1;
            }

            if (filter.OperatingSystems.Any())
            {
                result = (from product2 in result
                    join system in filter.OperatingSystems
                        on product2.Specification.OSType.ToString().ToLower() equals system
                    select product2);
            }

            return result.AsQueryable();
        }

        private async Task<List<ProductDto>> SelectToDto(IQueryable<Product> products)
        {
            return await products.Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Image = new ImageDto()
                {
                    Large = x.Image.Large,
                    Small = x.Image.Small
                },
                Specification = new SpecificationDto()
                {
                    Camera = x.Specification.Camera,
                    Manufacturer = x.Manufacturer.Name,
                    OSType = x.Specification.OSType.ToString(),
                    Storage = x.Specification.Storage
                }
            })
            .ToListAsync();
        }

        private List<ProductDto> SelectToDtoNot(IQueryable<Product> products)
        {
            return products.Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Image = new ImageDto()
                {
                    Large = x.Image.Large,
                    Small = x.Image.Small
                },
                Specification = new SpecificationDto()
                {
                    Camera = x.Specification.Camera,
                    Manufacturer = x.Manufacturer.Name,
                    OSType = x.Specification.OSType.ToString(),
                    Storage = x.Specification.Storage
                }
            }).ToList();
        }

        private int AddManufacturer(string Manufacturer)
        {
            var manufact = new Manufacturer()
            {
                Name = Manufacturer
            };
            _dbContext.Manufacturers.Add(manufact);
             _dbContext.SaveChanges();

            return manufact.Id;
        }

        private int AddImage(ImageDto image, int productAdd)
        {
            var img = new Image()
            {
                Id = productAdd,
                Large = image.Large,
                Small = image.Small
            };
            _dbContext.Images.Add(img);
             _dbContext.SaveChanges();

            return img.Id;
        }

        private int AddSpecification(SpecificationDto specs, int ManufacturerId,
            string osType, int productAdd)
        {
            var specification = new Specification()
            {
                Id = productAdd,
                Camera = specs.Camera,
                Storage = specs.Storage,
                OSType = (SystemType)Enum.Parse(typeof(SystemType), osType),
            };
            _dbContext.Specifications.Add(specification);
            _dbContext.SaveChanges();

            return specification.Id;
        }
    }

    public static class QueryString
    {
        public static IQueryable<Product> ByName(this IQueryable<Product> items, string name = null)
        {
            return !string.IsNullOrEmpty(name) ? items.Where(x => x.Name.Contains(name)) : items;
        }

        public static IQueryable<Product> IncludeData(this IQueryable<Product> items)
        {
            return items.Include(x => x.Manufacturer)
                .Include(x => x.Image)
                .Include(x => x.Specification);
        }
    }
}
