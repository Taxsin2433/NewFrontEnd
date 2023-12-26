using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CatalogTypeDto>> GetAllAsync()
        {
            var types = await _dbContext.CatalogTypes
                .Select(type => new CatalogTypeDto { Id = type.Id, Type = type.Type })
                .ToListAsync();

            return types;
        }

        public async Task<int?> AddAsync(CatalogTypeDto catalogType)
        {
            var newType = new CatalogType { Type = catalogType.Type };
            _dbContext.CatalogTypes.Add(newType);
            await _dbContext.SaveChangesAsync();
            return newType.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var type = await _dbContext.CatalogTypes.FindAsync(id);
            if (type == null)
            {
                return false; // Type not found
            }

            _dbContext.CatalogTypes.Remove(type);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
