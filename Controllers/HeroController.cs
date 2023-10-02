using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;
using elemental_heroes_server.Services.HeroService;
using Microsoft.AspNetCore.Mvc;

namespace elemental_heroes_server.Controllers
{
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
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> GetHero()
        {
            var response = await _heroService.GetHero();

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetHeroDto>>> UpdateHero(UpdateHeroDto updateHero)
        {
            UpdateHeroDto tmp = new UpdateHeroDto();
            var response = await _heroService.UpdateHero(tmp);

            return Ok(response);
        }
    }
}