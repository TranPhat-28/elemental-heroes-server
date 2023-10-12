using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace elemental_heroes_server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Hero> Heroes => Set<Hero>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
    }
}