using Sum.Model.Auth;

namespace Sum.Service.Interface
{
    public interface IUserService
    {
        AuthenticationResult Register(UserRegisterDto entity);
        AuthenticationResult Login(LoginDto entity);
    }
}       