using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;

namespace elemental_heroes_server.Services.HeroService
{
    public interface IHeroService
    {
        Task<ServiceResponse<GetHeroDto>> GetHero();
        Task<ServiceResponse<GetHeroDto>> AddHero(AddHeroDto newHero);
        Task<ServiceResponse<GetHeroDto>> UpdateHero(UpdateHeroDto updateHero);
        Task<ServiceResponse<GetHeroDto>> UpdateHeroName(UpdateHeroNameDto updateHeroName);
        Task<ServiceResponse<GetHeroDto>> EquipWeapon(EquipWeaponDto equipWeaponDto);
        Task<ServiceResponse<GetHeroDto>> RemoveWeapon();
        Task<ServiceResponse<GetHeroDto>> EquipSkill(EquipSkillDto equipSkillDto);
        Task<ServiceResponse<GetHeroDto>> RemoveSkill(RemoveSkillDto removeSkillDto);
    }
}