using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password, string confirmPassword);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}