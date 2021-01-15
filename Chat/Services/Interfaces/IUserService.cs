using Chat.Data.Dtos;
using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthenticateResponseDto>> Authenticate(AuthenticateDto dto);
        Task<ServiceResponse<User>> GetById(Guid id);
        Task<ServiceResponse<User>> CreateUser(AuthenticateDto dto);
    }
}
