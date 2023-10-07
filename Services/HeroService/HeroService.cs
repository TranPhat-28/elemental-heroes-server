using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeroService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetHeroDto>> AddHero(AddHeroDto newHero)
        {
            var response = new ServiceResponse<GetHeroDto>();
            var newHeroObj = _mapper.Map<Hero>(newHero);


            if (newHeroObj.Name == "")
            {
                response.IsSuccess = false;
                response.Message = "Missing Hero name";
                return response;
            }
            else if (newHeroObj.Attack + newHeroObj.Defense + newHeroObj.Hp > 100)
            {
                response.IsSuccess = false;
                response.Message = "Invalid stats setup";
                return response;
            }
            else
            {
                try
                {

                    // Add the authed UserId to the Obj
                    int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                    newHeroObj.UserId = userId;

                    // Add to the DB and save changes
                    _dataContext.Heroes.Add(newHeroObj);
                    await _dataContext.SaveChangesAsync();

                    // Return the newly created Hero
                    var createdHero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);

                    // Response
                    response.Data = _mapper.Map<GetHeroDto>(createdHero);
                    response.Message = "Hero created successfully";

                }
                catch (Exception e)
                {
                    response.IsSuccess = false;
                    response.Message = e.Message;
                    Console.WriteLine(e.Message);
                }
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> GetHero()
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);

                if (hero is null)
                {
                    response.Message = "No hero found";
                }
                else
                {
                    response.Data = _mapper.Map<GetHeroDto>(hero);
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> UpdateHero(UpdateHeroDto updateHero)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);
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

        public async Task<ServiceResponse<GetHeroDto>> UpdateHeroName(UpdateHeroNameDto updateHeroName)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);
                if (hero is null)
                {
                    throw new Exception("Cannot find hero");
                }
                else if (updateHeroName.Name == "")
                {
                    throw new Exception("Hero name must not be empty");
                }

                hero.Name = updateHeroName.Name;

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