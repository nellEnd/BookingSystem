using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Dtos
{
    public class UserDto
    {
    }

    public class LoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class SignUpDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class UpdateUserDto
    {
        public required string OldUsername { get; set; }
        public required string OldEmail { get; set; }
        public required string NewUsername { get; set; }
        public required string NewEmail { get; set; }
    }
}
