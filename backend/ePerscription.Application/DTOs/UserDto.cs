using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ePerscription.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; } = null!;
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
