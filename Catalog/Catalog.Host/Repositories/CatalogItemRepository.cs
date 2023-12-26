using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogItems.CountAsync();

            var items = await _dbContext.CatalogItems
                .OrderBy(item => item.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>
            {
                TotalCount = totalItems,
                Data = items
            };
        }

        public async Task<CatalogItem> GetByIdAsync(int id)
        {
            return (await _dbContext.CatalogItems.FindAsync(id)) !;
        }

        public async Task<PaginatedItems<CatalogItem>> GetByBrandPageAsync(string brand, int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogItems
                .Where(item => item.CatalogBrand.Brand == brand)
                .CountAsync();

            var items = await _dbContext.CatalogItems
                .Where(item => item.CatalogBrand.Brand == brand)
                .OrderBy(item => item.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>
            {
                TotalCount = totalItems,
                Data = items
            };
        }

        public async Task<PaginatedItems<CatalogItem>> GetByTypePageAsync(string type, int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogItems
                .Where(item => item.CatalogType.Type == type)
                .CountAsync();

            var items = await _dbContext.CatalogItems
                .Where(item => item.CatalogType.Type == type)
                .OrderBy(item => item.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogItem>
            {
                TotalCount = totalItems,
                Data = items
            };
        }

        public async Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            var newCatalogItem = new CatalogItem
            {
                Name = name,
                Description = description,
                Price = price,
                AvailableStock = availableStock,
                CatalogBrandId = catalogBrandId,
                CatalogTypeId = catalogTypeId,
                PictureFileName = pictureFileName
            };

            _dbContext.CatalogItems.Add(newCatalogItem);
            await _dbContext.SaveChangesAsync();

            return newCatalogItem.Id;
        }

        public async Task<bool> UpdateAsync(CatalogItem catalogItem)
        {
            _dbContext.CatalogItems.Update(catalogItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _dbContext.CatalogItems.FindAsync(id);
            if (item == null)
            {
                return false; // Item not found
            }

            _dbContext.CatalogItems.Remove(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
