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
        public Element Element { get; set; }
        public AttackType AttackType { get; set; }
        public DamageType DamageType { get; set; }
        public int Damage { get; set; }
        public string IconUrl { get; set; } = string.Empty;
        public List<User>? User { get; set; }
    }
}