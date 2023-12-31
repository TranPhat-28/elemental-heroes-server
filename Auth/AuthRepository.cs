using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace elemental_heroes_server.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();

            // Check if missing required fields
            if (email == "" || password == "")
            {
                response.IsSuccess = false;
                response.Message = "Missing required field(s)!";
                return response;
            }
            // Check if Email is valid
            else if (!EmailIsValid(email))
            {
                Console.WriteLine(email);
                response.IsSuccess = false;
                response.Message = "Invalid email address!";
                return response;
            }
            // Input fields OK
            else
            {
                // Look for the email in the DB
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

                // If none is found
                if (user is null)
                {
                    response.IsSuccess = false;
                    response.Message = "Email does not exist!";
                }
                // Wrong password
                else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.IsSuccess = false;
                    response.Message = "Incorrect password!";
                }
                // Correct password
                else
                {
                    response.Data = CreateJWTToken(user);
                    response.Message = "Login success";
                }

                return response;
            }
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
            // Check if Email is valid
            else if (!EmailIsValid(user.Email))
            {
                response.IsSuccess = false;
                response.Message = "Invalid email address!";
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

        public bool EmailIsValid(string email)
        {
            var regex = @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            if (Regex.Match(email, regex, RegexOptions.IgnoreCase).Success)
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

        private bool VerifyPasswordHash(string inputPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // Compute the hash of the input password
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputPassword));
                // Compare it with the one inside the DB
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateJWTToken(User user)
        {
            // The list of Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            // Getting the secret from AppSettings
            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;

            // Check if token is null
            if (appSettingsToken is null)
            {
                throw new Exception("AppSettings token is null!");
            }

            // Symmetric key for the token with secret is the AppSettings token
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            // For signing the token
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Storing some information such as Claims and Expiring day for the final token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            // JWT handler
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // Use the handler to create the token with the tokenDescriptor
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            // Write the token
            return tokenHandler.WriteToken(token);
        }
    }
}