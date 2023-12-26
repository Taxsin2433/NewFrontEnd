using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItems<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<CatalogItemDto> GetCatalogItemByIdAsync(int id);
    Task<IEnumerable<CatalogBrandDto>> GetCatalogBrandsAsync();
    Task<IEnumerable<CatalogTypeDto>> GetCatalogTypesAsync();
    Task<PaginatedItems<CatalogItemDto>> GetCatalogItemsByBrandAsync(string brand, int pageSize, int pageIndex);
    Task<PaginatedItems<CatalogItemDto>> GetCatalogItemsByTypeAsync(string type, int pageSize, int pageIndex);
    Task<int?> AddCatalogItemAsync(AddCatalogItemRequest request);
    Task<bool> UpdateCatalogItemAsync(int id, UpdateCatalogItemRequest request);
    Task<bool> DeleteCatalogItemAsync(int id);
    Task<int?> AddCatalogBrandAsync(CatalogBrandDto catalogBrand);
    Task<bool> DeleteCatalogBrandAsync(int id);
    Task<int?> AddCatalogTypeAsync(CatalogTypeDto catalogType);
    Task<bool> DeleteCatalogTypeAsync(int id);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}
