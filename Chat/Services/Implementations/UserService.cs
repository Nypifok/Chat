using Chat.Data.Authentication;
using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IDataContext context;

        public UserService(IOptions<AppSettings> appSettings, IDataContext context)
        {
            _appSettings = appSettings.Value;
            this.context = context;
        }

        public async Task<ServiceResponse<AuthenticateResponseDto>> Authenticate(AuthenticateDto dto)
        {
            await context.BeginTransaction();
            var user = context.Users.SingleOrDefault(x => x.Email == dto.Email && x.PasswordHash == dto.PasswordHash);

            if (user == null)
            {
                context.Rollback();
                return new ServiceResponse<AuthenticateResponseDto> { Success = false };
            }
            var token = generateJwtToken(user);
            await context.Commit();
            return new ServiceResponse<AuthenticateResponseDto> { Data = new AuthenticateResponseDto { Id = user.Id, Tag = user.Tag, Token = token } };
        }

        public async Task<ServiceResponse<User>> CreateUser(AuthenticateDto dto)
        {
            await context.BeginTransaction();
            try
            {
                var user = new User { PasswordHash=dto.PasswordHash,Email=dto.Email,Name=dto.Name,Tag=dto.Tag};

                await context.Users.AddAsync(user);
                await context.Commit();
                return new ServiceResponse<User> { Data = user };

            }
            catch (Exception ex)
            {
                await context.Rollback();
                return new ServiceResponse<User> { Message = ex.Message, Success = false };
            }
        }

        public async Task<ServiceResponse<User>> GetById(Guid id)
        {
            await context.BeginTransaction();
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                await context.Commit();
                return new ServiceResponse<User>{Data = user};
            }
            context.Rollback();
            return new ServiceResponse<User> { Success = false };
        }
        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
