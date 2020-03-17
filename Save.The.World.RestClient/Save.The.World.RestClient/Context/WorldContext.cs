using System;
using System.Linq;
using Save.The.World.RestClient.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;

namespace Save.The.World.RestClient.Context
{
    public class WorldContext : DbContext
    {

        public WorldContext(DbContextOptions<WorldContext> options) : base(options)
        {
            
        }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<CollectivePoints> CollectivePoints { get; set; }
    
    }
}
