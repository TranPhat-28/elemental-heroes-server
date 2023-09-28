using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.DTOs.UserAuthDtos
{
    public class UserRegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}