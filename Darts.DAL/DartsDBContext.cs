using Darts.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Darts.DAL
{
    public class DartsDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; } 

        public string DbPath { get; }

        public DartsDBContext(DbContextOptions<DartsDBContext> options) : base(options)
        { }
    }
}
