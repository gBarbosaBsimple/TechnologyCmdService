using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AbsanteeContext : DbContext
    {
        public virtual DbSet<TechnologyDataModel> Technology { get; set; }
        public AbsanteeContext(DbContextOptions<AbsanteeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TechnologyDataModel>().HasKey(v => v.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}