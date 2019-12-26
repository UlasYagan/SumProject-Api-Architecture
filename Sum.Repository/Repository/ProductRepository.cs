using Sum.Domain.Entities;
using Sum.Repository.Base;
using Sum.Repository.Interface;

namespace Sum.Repository.Repository
{
    public class ProductRepository : BaseRepository<Products, int>,IProductRepository
    {
        public ProductRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }
    }
}