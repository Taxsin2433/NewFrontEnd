using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogBffController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly ICatalogService _catalogService;

        public CatalogBffController(
            ILogger<CatalogBffController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaginatedItems<CatalogItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items(PaginatedItemsRequest request)
        {
            var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _catalogService.GetCatalogItemByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("brands")]
        [ProducesResponseType(typeof(IEnumerable<CatalogBrandDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _catalogService.GetCatalogBrandsAsync();
            return Ok(result);
        }

        [HttpGet("types")]
        [ProducesResponseType(typeof(IEnumerable<CatalogTypeDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTypes()
        {
            var result = await _catalogService.GetCatalogTypesAsync();
            return Ok(result);
        }

        [HttpGet("brand/{brand}")]
        [ProducesResponseType(typeof(PaginatedItems<CatalogItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByBrand(string brand, [FromQuery] PaginatedItemsRequest request)
        {
            var result = await _catalogService.GetCatalogItemsByBrandAsync(brand, request.PageSize, request.PageIndex);
            return Ok(result);
        }

        [HttpGet("type/{type}")]
        [ProducesResponseType(typeof(PaginatedItems<CatalogItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByType(string type, [FromQuery] PaginatedItemsRequest request)
        {
            var result = await _catalogService.GetCatalogItemsByTypeAsync(type, request.PageSize, request.PageIndex);
            return Ok(result);
        }
    }
}
