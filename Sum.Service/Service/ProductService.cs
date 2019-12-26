using Sum.Domain.Entities;
using Sum.Repository.Base;
using Sum.Repository.Interface;
using Sum.Service.Base;
using Sum.Service.Interface;

namespace Sum.Service.Service
{
    public class ProductService : BaseService<Products, int>, IProductRepository, IProductService
    {
        public ProductService(IBaseCrudRepository<Products, int> repository) : base(repository)
        {
        }
    }
}