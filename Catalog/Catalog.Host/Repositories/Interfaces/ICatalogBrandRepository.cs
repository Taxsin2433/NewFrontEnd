using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<IEnumerable<CatalogBrandDto>> GetAllAsync();
        Task<int?> AddAsync(CatalogBrandDto catalogBrand);
        Task<bool> DeleteAsync(int id);
    }
}
