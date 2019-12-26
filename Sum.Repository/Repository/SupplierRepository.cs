using Sum.Domain.Entities;
using Sum.Repository.Base;

namespace Sum.Repository.Repository
{
    public class SupplierRepository : BaseRepository<Suppliers, int>
    {
        public SupplierRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }
    }
}