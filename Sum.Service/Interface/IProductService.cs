using Sum.Domain.Entities;
using Sum.Model;
using Sum.Model.Dtos;
using Sum.Model.Options;
using System;
using System.Linq.Expressions;

namespace Sum.Service.Interface
{
    public interface IProductService
    {
        SumResultModel<ProductListDto> GetProductList(Expression<Func<Products, bool>> filter = null, DataPagingOptions dataPagingOptions = null);
        SumResultModel<ProductDto> GetProductById(int id);
        SumResultModel<ProductDto> CreateProduct(ProductDto entity);
        SumResultModel<ProductDto> UpdateProduct(ProductDto entity);
        SumResultModel<bool> DeleteProduct(int id);

    }
}               