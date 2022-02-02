using Microsoft.EntityFrameworkCore;
using OKRs.Core.Domain;

namespace OKRs.Web.Data
{
    public class ObjectivesDbContext : DbContext
    {
        public ObjectivesDbContext(DbContextOptions<ObjectivesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Objective> Objectives { get; set; }
        public DbSet<KeyResult> KeyResults { get; set; }
    }
}
