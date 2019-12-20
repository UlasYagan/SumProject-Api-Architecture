using Sum.Domain.Entity;
using Sum.Repository.Base;
using Sum.Repository.Interface;
using System;
using System.Linq;

namespace Sum.Repository.Repository
{
    public class UserRepository : BaseRepository<Users, Guid>, IUserRepository
    {
        public UserRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }


        public Users Login(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        public bool IsUserExist(string email)
        {
            return _dbContext.Users.Any(x => x.Email.Equals(email));
        }
    }
}
