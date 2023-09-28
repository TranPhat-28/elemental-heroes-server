using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.UserAuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace elemental_heroes_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login()
        {
            var result = await _authRepository.Login("email", "password");
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepository.Register(new User { Email = request.Email }, request.Password, request.ConfirmPassword);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}