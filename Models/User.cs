using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public Hero? Hero { get; set; }
        public List<Skill>? Skills { get; set; }
        public List<Weapon>? Weapons { get; set; }
        public int Balance { get; set; } = 1000;
    }
}