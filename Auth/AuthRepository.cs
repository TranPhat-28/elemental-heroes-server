using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using Microsoft.EntityFrameworkCore;

namespace elemental_heroes_server.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;

        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            response.Data = $"Token for {email}";

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password, string confirmPassword)
        {
            // The ServiceReponse
            var response = new ServiceResponse<int>();

            // Check if missing Email or Password or ConfirmPassword
            if (user.Email == "" || password == "" || confirmPassword == "")
            {
                response.IsSuccess = false;
                response.Message = "Missing required fields!";
                return response;
            }
            // Check if Password and Confirm does not match
            else if (password != confirmPassword)
            {
                response.IsSuccess = false;
                response.Message = "Password does not match!";
                return response;
            }
            // Check if user already existed
            else if (await UserExists(user.Email))
            {
                response.IsSuccess = false;
                response.Message = "Email already existed!";
                return response;
            }
            // Else perform register
            else
            {
                // Call the CreateHash method to generate the hashed password
                CreateHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                // Setting the password to the user obj
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                // Apply the changes to the DB
                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();

                response.Data = user.Id;
                response.Message = "Registration completed! Please login";

                return response;
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _dataContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateHash(string plainPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainPassword));
            }
        }
    }
}