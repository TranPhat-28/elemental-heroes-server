using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;

namespace elemental_heroes_server.Services.HeroService
{
    public class HeroService : IHeroService
    {
        public async Task<ServiceResponse<GetHeroDto>> AddHero(AddHeroDto newHero)
        {
            var response = new ServiceResponse<GetHeroDto>();
            response.Message = "Add a new Hero";

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> GetHero()
        {
            var response = new ServiceResponse<GetHeroDto>();
            response.Message = "Get Hero information";

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> UpdateHero(UpdateHeroDto updateHero)
        {
            var response = new ServiceResponse<GetHeroDto>();
            response.Message = "Update current Hero";

            return response;
        }
    }
}