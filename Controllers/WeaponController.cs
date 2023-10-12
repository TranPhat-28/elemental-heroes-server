using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.WeaponDtos;
using elemental_heroes_server.Services.WeaponService;
using Microsoft.AspNetCore.Mvc;

namespace elemental_heroes_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> GetAllWeapons()
        {
            var response = await _weaponService.GetAllWeapons();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        // User cannot perform this action
        // [HttpPost]
        // public async Task<ActionResult<ServiceResponse<GetWeaponDto>>> AddSkill(AddWeaponDto newWeapon)
        // {
        //     var response = await _weaponService.AddWeapon(newWeapon);

        //     if (!response.IsSuccess)
        //     {
        //         return BadRequest(response);
        //     }
        //     else
        //     {
        //         return Ok(response);
        //     }
        // }
    }
}