using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<IEnumerable<CatalogTypeDto>> GetAllAsync();
        Task<int?> AddAsync(CatalogTypeDto catalogType);
        Task<bool> DeleteAsync(int id);
    }
}
