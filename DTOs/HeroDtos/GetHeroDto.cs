using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.SkillDtos;
using elemental_heroes_server.DTOs.UserSkillDtos;

namespace elemental_heroes_server.DTOs.HeroDtos
{
    public class GetHeroDto
    {
        public int Id { get; set; }
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
        public int UserId { get; set; }
        public GetWeaponDto? Weapon { get; set; }
        public GetSkillDto? SkillA { get; set; }
        public GetSkillDto? SkillB { get; set; }
        public GetSkillDto? SkillC { get; set; }
    }
}