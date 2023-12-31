using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.UserWeaponDtos;
using elemental_heroes_server.DTOs.WeaponDtos;

namespace elemental_heroes_server.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetOwnedWeaponList>> GetAllWeapons();
        Task<ServiceResponse<GetWeaponDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}