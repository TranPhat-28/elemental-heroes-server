using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Services.OpenChestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elemental_heroes_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OpenChestController : ControllerBase
    {
        private readonly IOpenChestService _openChestService;

        public OpenChestController(IOpenChestService openChestService)
        {
            _openChestService = openChestService;
        }

        [HttpGet("OpenWeaponChest")]
        public async Task<ActionResult<ServiceResponse<int[]>>> OpenWeaponChest()
        {
            var response = await _openChestService.OpenWeaponChest();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("OpenSkillChest")]
        public async Task<ActionResult<ServiceResponse<int[]>>> OpenSkillChest()
        {
            
            var response = await _openChestService.OpenSkillChest();

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