using System;
using Sum.Model.Auth;

namespace Sum.Service.Interface
{
    public interface IAuthService
    {
        AuthenticationResult Login(string email, string password);
        AuthenticationResult RePassword(Guid id, string password);
        AuthenticationResult ResetPassword(ResetPasswordDto model);
        AuthenticationResult AccountActivetion(Guid id);
        AuthenticationResult ForgottenPassword(string email);
    }
}