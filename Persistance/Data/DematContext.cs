#nullable disable
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data
{
    public class DematContext : DbContextBase , IDematContext
    {
        public DematContext(DbContextOptions<DematContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<User> Users { get; set; }



        /// <summary>
        /// On model creating
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DematContext).Assembly);
            modelBuilder.Entity<Test>().HasKey(t => new { t.Id});

        }

        /// <summary>
        /// Called before save changes.
        /// </summary>
        protected override void OnBeforeSaveChanges()
        {
            UseAuditable();
            UseSoftDelete();
            base.OnBeforeSaveChanges();
        }

    }
}
