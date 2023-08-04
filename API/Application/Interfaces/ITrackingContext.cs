using Application.DInterfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ITrackingContext : IContext
    {
        public DbSet<Navigator> Navigators { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<ShipmentHighlight> ShipmentHighlights { get; set; }
    }
}
