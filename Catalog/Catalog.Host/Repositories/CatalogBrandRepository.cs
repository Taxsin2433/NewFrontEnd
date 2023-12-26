using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogBrandRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CatalogBrandDto>> GetAllAsync()
        {
            var brands = await _dbContext.CatalogBrands
                .Select(brand => new CatalogBrandDto { Id = brand.Id, Brand = brand.Brand })
                .ToListAsync();

            return brands;
        }

        public async Task<int?> AddAsync(CatalogBrandDto catalogBrand)
        {
            var newBrand = new CatalogBrand { Brand = catalogBrand.Brand };
            _dbContext.CatalogBrands.Add(newBrand);
            await _dbContext.SaveChangesAsync();
            return newBrand.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _dbContext.CatalogBrands.FindAsync(id);
            if (brand == null)
            {
                return false; // Brand not found
            }

            _dbContext.CatalogBrands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
