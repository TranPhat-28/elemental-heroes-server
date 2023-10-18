using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.WeaponDtos;

namespace elemental_heroes_server.DTOs.UserWeaponDtos
{
    public class GetOwnedWeaponList
    {
        public int Id { get; set; }
        public int TotalWeaponCount { get; set; } = 0;
        public List<GetWeaponDto>? Weapons { get; set; }
    }
}