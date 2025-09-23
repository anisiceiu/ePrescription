using AutoMapper;
using ePerscription.Application.DTOs;
using ePerscription.Application.Interfaces;
using ePerscription.Domain.Entities;
using ePerscription.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePerscription.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository  _accountRepository;
        public AccountService(IAccountRepository  accountRepository, IMapper mapper)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }
        public async Task<UserDto> Login(string username, string password)
        {
            return _mapper.Map<UserDto>(await _accountRepository.Login(username, password));
        }

        public async Task<UserDto> Register(UserDto user, string password)
        {
            return _mapper.Map<UserDto>(await _accountRepository.Register(_mapper.Map<User>(user), password));
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            throw new NotImplementedException();
        }
    }
}
