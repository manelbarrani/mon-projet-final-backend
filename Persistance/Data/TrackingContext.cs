using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data
{
    public class TrackingContext : DbContextBase , ITrackingContext
    {
        public TrackingContext(DbContextOptions<TrackingContext> options) : base(options) { }

        public DbSet<Navigator> Navigators { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<ShipmentHighlight> ShipmentHighlights { get; set; }


        /// <summary>
        /// On model creating
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrackingContext).Assembly);
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
