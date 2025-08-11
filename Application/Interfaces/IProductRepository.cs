using Domain.Common;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PagedList<Product>> GetAllWithTypesAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken);
        Task<Product?> GetById(Guid productId);
        Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);  // Retourne Product?
        Task SoftDelete(Guid productId);
    }
}
