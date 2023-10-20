using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;
using elemental_heroes_server.Services.HeroService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elemental_heroes_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> AddHero(AddHeroDto newHero)
        {
            var response = await _heroService.AddHero(newHero);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> GetHero()
        {
            var response = await _heroService.GetHero();
            return Ok(response);
        }

        // [HttpPut]
        // public async Task<ActionResult<ServiceResponse<GetHeroDto>>> UpdateHero(UpdateHeroDto updateHero)
        // {
        //     var response = await _heroService.UpdateHero(updateHero);
        //     return Ok(response);
        // }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> UpdateHeroName(UpdateHeroNameDto updateHeroName)
        {
            var response = await _heroService.UpdateHeroName(updateHeroName);
            return Ok(response);
        }

        [HttpPut("EquipWeapon")]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> EquipWeapon(EquipWeaponDto equipWeaponDto)
        {
            var response = await _heroService.EquipWeapon(equipWeaponDto);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPut("RemoveWeapon")]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> RemoveWeaponn()
        {
            var response = await _heroService.RemoveWeapon();

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