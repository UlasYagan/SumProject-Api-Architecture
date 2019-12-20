using System;
using System.Linq;
using Sum.Domain.Entity;
using Sum.Model.Auth;
using Sum.Repository.Base;
using Sum.Repository.Interface;
using Sum.Service.Features;
using Sum.Service.Helper;
using Sum.Service.Interface;

namespace Sum.Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseCrudRepository<Users, Guid> _baseCrudRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailService;

        public AuthService(
            IUserRepository userRepository,
            IBaseCrudRepository<Users, Guid> baseCrudRepository,
            IEmailSender emailService)
        {
            _userRepository = userRepository;
            _baseCrudRepository = baseCrudRepository;
            _emailService = emailService;
        }

        public AuthenticationResult Login(string email, string password)
        {
            password = Help.Hashing(password);
            var user = _userRepository.Login(email, password);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    ReadableMessage = "Emailiniz veya Şifreniz hatalıdır."
                };
            }

            return new AuthenticationResult
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Success = true,
                ReadableMessage = "İşlem Başarılı",
            };
        }


        public AuthenticationResult ForgottenPassword(string email)
        {
            email = email.Trim();
            var user = _baseCrudRepository.Get(x => x.Email.Equals(email)).FirstOrDefault();
            if (user != null)
            {
                string to = email;
                string subject = "Şifre Yenileme";
                string body = "Merhaba " + user.FullName + ",\n\n" +
                              "Yeni şifrenizi belirleyebilmek için aşağıdaki linke tıklayınız. \n\n http://www.sum.com.tr/Account/ResetPassword/" + user.Id;

                _emailService.SendEmail(to, subject, body, null);
                return new AuthenticationResult
                {
                    Success = true,
                    ReadableMessage = "Şifre yenileme linki email adresinize gönderilmiştir."
                };
            }
            return new AuthenticationResult
            {
                Success = false,
                ReadableMessage = "Bu Email adresi bulunamamıştır."
            };
        }

        public bool AccountApproveSendMail(Guid id)
        {
            var result = _baseCrudRepository.GetById(id);
            if (result != null)
            {
                string to = result.Email;
                string subject = "Üyelik Aktivasyonu";
                string body = "Merhaba " + result.FullName + ",\n\n" +
                              "Üyeliğinizi aktif hale getirmek için aşağıdaki linke tıklayınız. \n\n http://www.sum.com.tr/Auth/AccountActivation/" + result.Id;

                _emailService.SendEmail(to, subject, body, null);
                return true;
            }
            return false;
        }

        public AuthenticationResult ResetPassword(ResetPasswordDto model)
        {
            var user = _baseCrudRepository.GetById(model.UserId);
            if (user.Password != Help.Hashing(model.OldPassword))
            {
                return new AuthenticationResult()
                {
                    Success = false,
                    ReadableMessage = "Eski şifre yanlış"
                };
            }

            user.Password = Help.Hashing(model.NewPassword);
            var result = _baseCrudRepository.Update(user);
            if (result != null)
            {
                return new AuthenticationResult
                {
                    Success = true,
                    ReadableMessage = "Şifreniz başarıyla değiştirilmiştir."
                };
            }
            return new AuthenticationResult()
            {
                Success = false,
                ReadableMessage = "İşlem sırasında bir hata oluştu tekrar deneyiniz."
            };
        }

        public AuthenticationResult AccountActivetion(Guid id)
        {
            Users user = _baseCrudRepository.GetById(id);
            user.StatusId = 1;
            //user.IsActive = true;
            var item = _baseCrudRepository.Update(user);
            if (item != null)
            {
                return new AuthenticationResult
                {
                    Success = true,
                    ReadableMessage = "İşlem Başarılı şekilde kaydedilmiştir"
                };
            }

            return new AuthenticationResult
            {
                Success = false,
                ReadableMessage = "İşlem sırasında bir hata oluştu lütfen tekrar deneyin"
            };

        }

        private bool IsUserExist(string email)
        {
            return _userRepository.IsUserExist(email);
        }

        public AuthenticationResult RePassword(Guid id, string password)
        {
            throw new NotImplementedException();
        }
    }
}