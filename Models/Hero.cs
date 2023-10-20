using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.WeaponDtos;

namespace elemental_heroes_server.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Hero";
        public Element Element { get; set; } = Element.Fire;
        public int Hp { get; set; } = 10;
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public AttackType AttackType { get; set; } = AttackType.Melee;
        public DamageType DamageType { get; set; } = DamageType.Physical;
        public User? User { get; set; }
        public int UserId { get; set; }
        public Weapon? Weapon { get; set; }
    }
}