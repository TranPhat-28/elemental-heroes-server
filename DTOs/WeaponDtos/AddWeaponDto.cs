using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.DTOs.WeaponDtos
{
    public class AddWeaponDto
    {
        public string Name { get; set; } = "New Weapon";
        public Element Element { get; set; } = Element.Fire;
        public string IconUrl { get; set; } = string.Empty;
    }
}