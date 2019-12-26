using Sum.Domain.Entities;
using Sum.Repository.Base;

namespace Sum.Repository.Repository
{
    public class CategoryRepository : BaseRepository<Categories, int>
    {
        public CategoryRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }
    }
}