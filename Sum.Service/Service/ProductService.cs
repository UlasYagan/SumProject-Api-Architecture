using Sum.Domain.Entities;
using Sum.Model;
using Sum.Model.Dtos;
using Sum.Model.Options;
using Sum.Repository.Base;
using Sum.Repository.Interface;
using Sum.Service.Base;
using Sum.Service.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sum.Service.Service
{
    public class ProductService : BaseService<Products, int>, IProductRepository, IProductService
    {
        private readonly IBaseCrudRepository<Suppliers, int> _baseCrudSupplierRepository;
        private readonly IBaseCrudRepository<Categories, int> _baseCrudCategoryRepository;
        public ProductService(
            IBaseCrudRepository<Products, int> repository, 
            IBaseCrudRepository<Categories, int> baseCrudCategoryRepository, 
            IBaseCrudRepository<Suppliers, int> baseCrudSupplierRepository) : base(repository)
        {
            _baseCrudCategoryRepository = baseCrudCategoryRepository;
            _baseCrudSupplierRepository = baseCrudSupplierRepository;
        }


        public SumResultModel<ProductListDto> GetProductList(Expression<Func<Products, bool>> filter = null, DataPagingOptions dataPagingOptions = null)
        {
            SumResultModel<ProductListDto> result = new SumResultModel<ProductListDto>();

            var list = _repository.Get(filter, dataPagingOptions).Select(c=> new ProductListDto
            {
                ProductId = c.ProductId,
                CategoryName = c.CategoryId != null ? _baseCrudCategoryRepository.GetById((int)c.CategoryId).CategoryName : string.Empty,
                ProductName = c.ProductName,
                QuantityPerUnit = c.QuantityPerUnit,
                SupplierName = c.SupplierId != null ? _baseCrudSupplierRepository.GetById((int)c.SupplierId).CompanyName : string.Empty,
                UnitPrice = c.UnitPrice,
                UnitsInStock = c.UnitsInStock
            }).ToList();
            
            result.Data = list; 
            result.Success = true;
            result.StatusCode = 200;
            result.TotalRecordCount = _repository.GetTotalRecordCount();

            return result;
        }

        public SumResultModel<ProductDto> GetProductById(int id)
        {   
            SumResultModel<ProductDto> result = new SumResultModel<ProductDto>();

            var product = ProductById(id);
            if (product != null)
            {
                result.SingleData = product;
                result.Success = true;
                result.StatusCode = 200;
            }
            else
            {
                result.Success = false;
                result.ReadableMessage = "Product is not found";
                result.StatusCode = 204;
            }

            return result;
        }

        public SumResultModel<ProductDto> CreateProduct(ProductDto entity)
        {
            SumResultModel<ProductDto> result = new SumResultModel<ProductDto>();

            Products product = new Products
            {
                ProductName = entity.ProductName,
                CategoryId = entity.CategoryId,
                SupplierId = entity.SupplierId,
                QuantityPerUnit = entity.QuantityPerUnit,
                Discontinued = entity.Discontinued,
                UnitPrice = entity.UnitPrice,
                ReorderLevel = entity.ReorderLevel,
                UnitsInStock = entity.UnitsInStock,
                UnitsOnOrder = entity.UnitsOnOrder
            };

           var item = _repository.Create(product);
           if (item != null)
           {
               result.SingleData = ProductById(item.ProductId);
               result.Success = true;
               result.StatusCode = 200;
               result.ReadableMessage = "Process is successfully";
           }
           else
           {
               result.Success = false;
               result.StatusCode = 500;
               result.ReadableMessage = "Something went wrong";
           }

           return result;
        }

        public SumResultModel<ProductDto> UpdateProduct(ProductDto entity)
        {
            SumResultModel<ProductDto> result = new SumResultModel<ProductDto>();

            Products product = new Products
            {
                ProductName = entity.ProductName,
                CategoryId = entity.CategoryId,
                SupplierId = entity.SupplierId,
                QuantityPerUnit = entity.QuantityPerUnit,
                Discontinued = entity.Discontinued,
                UnitPrice = entity.UnitPrice,
                ReorderLevel = entity.ReorderLevel,
                UnitsInStock = entity.UnitsInStock,
                UnitsOnOrder = entity.UnitsOnOrder
            };

            var item = _repository.Update(product);
            if (item != null)
            {
                result.SingleData = ProductById(item.ProductId);
                result.Success = true;
                result.StatusCode = 200;
                result.ReadableMessage = "Process is successfully";
            }
            else
            {
                result.Success = false;
                result.StatusCode = 500;
                result.ReadableMessage = "Something went wrong";
            }

            return result;
        }

        public SumResultModel<bool> DeleteProduct(int id)
        {
            SumResultModel<bool> result = new SumResultModel<bool>();
            var product = _repository.GetById(id);
            if (product != null)
            {
                result.SingleData = true;
                result.Success = true;
                result.StatusCode = 200;
                result.ReadableMessage = "Process is successfully";
            }
            else
            {
                result.Success = false;
                result.ReadableMessage = "Product is not found";
                result.StatusCode = 204;
            }

            return result;
        }

        public ProductDto ProductById(int id)
        {
            ProductDto item = new ProductDto();
            var product = _repository.GetById(id);
            if (product != null)
            {
               item = new ProductDto
                {
                    ProductId = product.ProductId,
                    SupplierId = product.SupplierId,
                    CategoryId = product.CategoryId,
                    ProductName = product.ProductName,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitsInStock = product.UnitsInStock,
                    UnitPrice = product.UnitPrice,
                    Discontinued = product.Discontinued,
                    ReorderLevel = product.ReorderLevel,
                    UnitsOnOrder = product.UnitsOnOrder
                };
            }
            return item;
        }
    }
}




















