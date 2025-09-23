using ePerscription.Domain.Entities;
using ePerscription.Domain.Interfaces;
using ePerscription.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ePerscription.Infrastructure.Repositories.AccountRepository;

namespace ePerscription.Infrastructure.Repositories
{

    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        private readonly EPrescriptionContext _context;

        public AccountRepository(EPrescriptionContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            // Validate
            if (await _context.Users.Where(u => u.Username == user.Username).AnyAsync())
                throw new Exception("Username already exists");

            if (await _context.Users.Where(u => u.Email == user.Email).AnyAsync())
                throw new Exception("Email already exists");

            // Create password hash and salt
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.CreatedAt = DateTime.UtcNow;

            // Save user
            await AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(string Email, string password)
        {
            var user = await _context.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Invalid password");

            if (!user.IsActive)
                throw new Exception("User account is inactive");

            // Update last login
            user.LastLogin = DateTime.UtcNow;

            return user;
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}

