using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New skill";
        public Element Element { get; set; } = Element.Fire;
        public AttackType AttackType { get; set; } = AttackType.Melee;
        public DamageType DamageType { get; set; } = DamageType.Physical;
        public int Damage { get; set; } = 10;
        public string IconUrl { get; set; } = string.Empty;
        public List<User>? Users { get; set; }
    }
}