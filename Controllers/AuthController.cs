using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var result = await _authRepository.Login("username", "password");
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register()
        {
            var result = await _authRepository.Register(new User { Email = "DemoEmail" }, "DemoPassword");
            return Ok(result);
        }
    }
}