using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<CatalogItem> GetByIdAsync(int id);
    Task<PaginatedItems<CatalogItem>> GetByBrandPageAsync(string brand, int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItem>> GetByTypePageAsync(string type, int pageIndex, int pageSize);
    Task<int?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> UpdateAsync(CatalogItem catalogItem);
    Task<bool> DeleteAsync(int id);
}
