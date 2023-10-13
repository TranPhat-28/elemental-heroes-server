using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.SkillDtos;
using elemental_heroes_server.DTOs.WeaponDtos;

namespace elemental_heroes_server.Services.OpenChestService
{
    public interface IOpenChestService
    {
        Task<ServiceResponse<List<GetWeaponDto>>> OpenWeaponChest();
        Task<ServiceResponse<List<GetSkillDto>>> OpenSkillChest();
    }
}