using Application.DInterfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ITrackingContext : IContext
    {
        public DbSet<Navigator> Navigators { get; set; }
    }
}
