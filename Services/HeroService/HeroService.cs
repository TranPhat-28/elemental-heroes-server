using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.HeroDtos;
using Microsoft.EntityFrameworkCore;

namespace elemental_heroes_server.Services.HeroService
{
    public class HeroService : IHeroService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public HeroService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetHeroDto>> AddHero(AddHeroDto newHero)
        {
            var response = new ServiceResponse<GetHeroDto>();
            var newHeroObj = _mapper.Map<Hero>(newHero);
            try
            {
                _dataContext.Heroes.Add(newHeroObj);
                await _dataContext.SaveChangesAsync();

                // Return the newly created Hero
                var createdHero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.Name == newHeroObj.Name);

                // Response
                response.Data = _mapper.Map<GetHeroDto>(createdHero);
                response.Message = "Hero created successfully";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> GetHero(int userId)
        {
            var response = new ServiceResponse<GetHeroDto>();
            response.Message = "Get Hero information";

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> UpdateHero(UpdateHeroDto updateHero)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.Name == updateHero.Name);
                if (hero is null)
                {
                    throw new Exception("Cannot find hero");
                }

                hero.Name = updateHero.Name;
                hero.Element = updateHero.Element;
                hero.Hp = updateHero.Hp;
                hero.Attack = updateHero.Attack;
                hero.Defense = updateHero.Defense;
                hero.AttackType = updateHero.AttackType;
                hero.DamageType = updateHero.DamageType;

                // Save the changes
                await _dataContext.SaveChangesAsync();

                // Return the newly updated hero
                response.Data = _mapper.Map<GetHeroDto>(hero);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }
    }
}