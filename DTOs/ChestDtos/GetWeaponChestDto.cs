using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.WeaponDtos;

namespace elemental_heroes_server.DTOs.ChestDtos
{
    public class GetWeaponChestDto
    {
        public List<GetWeaponDto>? ObtainedWeapons { get; set; }
        public int RemainingBalance { get; set; }
    }
}