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

        // This is for the Users table
        public DbSet<User> Users => Set<User>();
        public DbSet<Hero> Heroes => Set<Hero>();
    }
}