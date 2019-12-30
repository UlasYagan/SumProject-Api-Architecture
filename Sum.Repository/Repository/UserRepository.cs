using System;
using Sum.Domain.Entities;
using Sum.Repository.Base;
using Sum.Repository.Interface;

namespace Sum.Repository.Repository
{
    public class UserRepository : BaseRepository<Users, Guid>, IUserRepository
    {
        public UserRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }
    }
}