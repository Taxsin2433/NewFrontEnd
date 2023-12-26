using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogItemRepository catalogItemRepository,
            ICatalogBrandRepository catalogBrandRepository,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _catalogBrandRepository = catalogBrandRepository;
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex)
        {
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
            return new PaginatedItems<CatalogItemDto>
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<CatalogItemDto> GetCatalogItemByIdAsync(int id)
        {
            var catalogItem = await _catalogItemRepository.GetByIdAsync(id);
            return _mapper.Map<CatalogItemDto>(catalogItem);
        }

        public async Task<IEnumerable<CatalogBrandDto>> GetCatalogBrandsAsync()
        {
            return await _catalogBrandRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CatalogTypeDto>> GetCatalogTypesAsync()
        {
            return await _catalogTypeRepository.GetAllAsync();
        }

        public async Task<PaginatedItems<CatalogItemDto>> GetCatalogItemsByBrandAsync(string brand, int pageSize, int pageIndex)
        {
            var result = await _catalogItemRepository.GetByBrandPageAsync(brand, pageIndex, pageSize);
            return new PaginatedItems<CatalogItemDto>
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedItems<CatalogItemDto>> GetCatalogItemsByTypeAsync(string type, int pageSize, int pageIndex)
        {
            var result = await _catalogItemRepository.GetByTypePageAsync(type, pageIndex, pageSize);
            return new PaginatedItems<CatalogItemDto>
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<int?> AddCatalogItemAsync(AddCatalogItemRequest request)
        {
            return await _catalogItemRepository.AddAsync(request));
        }

        public async Task<bool> UpdateCatalogItemAsync(int id, UpdateCatalogItemRequest request)
        {
            var catalogItem = await _catalogItemRepository.GetByIdAsync(id);
            if (catalogItem == null)
            {
                return false; // Item not found
            }

            catalogItem.Name = request.Name;
            catalogItem.Description = request.Description;
            catalogItem.Price = request.Price;
            catalogItem.AvailableStock = request.AvailableStock;
            catalogItem.CatalogBrandId = request.CatalogBrandId;
            catalogItem.CatalogTypeId = request.CatalogTypeId;
            catalogItem.PictureFileName = request.PictureFileName;

            return await _catalogItemRepository.UpdateAsync(catalogItem);
        }

        public async Task<bool> DeleteCatalogItemAsync(int id)
        {
            return await _catalogItemRepository.DeleteAsync(id);
        }

        public async Task<int?> AddCatalogBrandAsync(CatalogBrandDto catalogBrand)
        {
            return await _catalogBrandRepository.AddAsync(catalogBrand);
        }

        public async Task<bool> DeleteCatalogBrandAsync(int id)
        {
            return await _catalogBrandRepository.DeleteAsync(id);
        }

        public async Task<int?> AddCatalogTypeAsync(CatalogTypeDto catalogType)
        {
            return await _catalogTypeRepository.AddAsync(catalogType);
        }

        public async Task<bool> DeleteCatalogTypeAsync(int id)
        {
            return await _catalogTypeRepository.DeleteAsync(id);
        }

        public Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
        {
            return ExecuteSafeAsync(() => _catalogItemRepository.AddAsync(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName));
        }
    }
}
