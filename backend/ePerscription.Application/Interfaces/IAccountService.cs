using ePerscription.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Application.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto> Register(UserDto user, string password);
        Task<UserDto> Login(string username, string password);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
