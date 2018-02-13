using Microsoft.EntityFrameworkCore;
using OKRs.Models;

namespace OKRs.Data
{
    public class ObjectivesDbContext : DbContext
    {
        public ObjectivesDbContext(DbContextOptions<ObjectivesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Objective> Objectives { get; set; }
        public DbSet<KeyResult> KeyResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
