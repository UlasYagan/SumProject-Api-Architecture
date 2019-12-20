using Sum.Domain.Entity;

namespace Sum.Repository.Interface
{
    public interface IUserRepository
    {
        Users Login(string email, string password);
        bool IsUserExist(string email);
    }
}