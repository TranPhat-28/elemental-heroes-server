using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.DTOs.GameDtos
{
    public class GetBotHeroDto
    {
        public string Name { get; set; } = string.Empty;
        public Element Element { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int BonusHp { get; set; }
        public int BonusAttack { get; set; }
        public int BonusDefense { get; set; }
        public AttackType AttackType { get; set; }
        public DamageType DamageType { get; set; }
        public GetWeaponDto? Weapon { get; set; }
    }
}