using Microsoft.IdentityModel.Tokens;
using Sum.Domain.Entities;
using Sum.Model.Auth;
using Sum.Model.Options;
using Sum.Repository.Base;
using Sum.Service.Base;
using Sum.Service.Helper;
using Sum.Service.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Sum.Service.Service
{
    public class UserService : BaseService<Users, Guid>, IUserService
    {
        public readonly JwtSettings _jwtSettings;
        public UserService(IBaseCrudRepository<Users, Guid> repository, JwtSettings jwtSettings) : base(repository)
        {
            _jwtSettings = jwtSettings;
        }


        public AuthenticationResult Register(UserRegisterDto entity)
        {
            var existUser = _repository.Get(c => c.Email.Equals(entity.Email));

            if (existUser.Any())
            {
                return new AuthenticationResult
                {
                    ReadableMessage = "This email address already exists",
                    Success = false
                };
            }

            Users user = new Users
            {
                Id = Guid.NewGuid(),
                FirstName = entity.FirstName.Trim(),
                LastName = entity.LastName.Trim(),
                Email = entity.Email.Trim(),
                Password = Help.Hashing(entity.Password.Trim())
            };

            var createdUser = _repository.Create(user);
            return new AuthenticationResult
            {
                Id = createdUser.Id,
                ReadableMessage = "The Process is success",
                Email = createdUser.Email,
                Success = true,
                FullName = $"{createdUser.FirstName} {createdUser.LastName}",
                Token = GenerateToken(createdUser)
            };
        }

        public AuthenticationResult Login(LoginDto entity)
        {
            var user = _repository.Get(c => c.Email.Equals(entity.Email) && c.Password == Help.Hashing(entity.Password.Trim())).FirstOrDefault();

            if (user == null)
            {
                return new AuthenticationResult
                {
                    ReadableMessage = "User/Password is wrong",
                    Success = false
                };
            }
            return new AuthenticationResult
            {
                Id = user.Id,
                ReadableMessage = "The Process is success",
                Email = user.Email,
                Success = true,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = GenerateToken(user)
            };
        }

        public string GenerateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()), 
                    new Claim("id",user.Id.ToString()), 
                     
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}