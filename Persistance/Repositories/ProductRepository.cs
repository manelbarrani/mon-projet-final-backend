using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DematContext _dbContext;

        public ProductRepository(DematContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        public async Task<Product?> GetById(Guid productId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task SoftDelete(Guid productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                product.IsDeleted = true;  // Assure-toi que ta classe Product possède bien cette propriété
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedList<Product>> GetAllWithTypesAsync(int? pageNumber, int? pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.Products
                .AsNoTracking()
                .Where(p => !p.IsDeleted); // Filtrer les produits non supprimés

            int currentPage = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 10;

            int totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<Product>(items, totalCount, currentPage, currentPageSize);
        }
    }
}
