using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.Auth
{
    public class AuthRepository : IAuthRepository
    {
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            response.Data = $"Token for {username}";

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            response.Data = 1;
            response.Message = user.Email + ":" +  password;

            return response;
        }

        public Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }
    }
}