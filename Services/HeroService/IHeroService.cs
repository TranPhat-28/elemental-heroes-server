using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;

namespace elemental_heroes_server.Services.HeroService
{
    public interface IHeroService
    {
        Task<ServiceResponse<GetHeroDto>> GetHero(int userId);
        Task<ServiceResponse<GetHeroDto>> AddHero(AddHeroDto newHero);
        Task<ServiceResponse<GetHeroDto>> UpdateHero(UpdateHeroDto updateHero);
    }
}