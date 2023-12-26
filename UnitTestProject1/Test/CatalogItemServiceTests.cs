using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Test
{
    using NUnit.Framework;
    using Moq;
    using System.Threading.Tasks;
    using Catalog.Host.Data;
    using Catalog.Host.Models.Dtos;
    using Catalog.Host.Repositories.Interfaces;
    using Catalog.Host.Services;

    [TestFixture]
    public class CatalogItemServiceTests
    {
        [Test]
        public async Task GetCatalogItemByIdAsync_ShouldReturnCatalogItemDto()
        {

            var itemId = 1;
            var mockCatalogItemRepository = new Mock<ICatalogItemRepository>();
            var mockMapper = new Mock<AutoMapper.IMapper>();

            var mockCatalogItem = new CatalogItem
            {
                Id = itemId,
                Name = "TestItem",
                Description = "TestDescription",
                Price = 19.99m,
                AvailableStock = 100,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                PictureUrl = "test.jpg"
        
            };

            var mockCatalogItemDto = new CatalogItemDto
            {
                Id = itemId,
                Name = "TestItem",
                Description = "TestDescription",
                Price = 19.99m,
                AvailableStock = 100,
                CatalogBrand = new CatalogBrandDto { Id = 1, Brand = "TestBrand" },
                CatalogType = new CatalogTypeDto { Id = 1, Type = "TestType" },
                PictureUrl = "test.jpg"

            };

            mockCatalogItemRepository.Setup(repo => repo.GetByIdAsync(itemId)).ReturnsAsync(mockCatalogItem);
            mockMapper.Setup(mapper => mapper.Map<CatalogItemDto>(mockCatalogItem)).Returns(mockCatalogItemDto);

            var catalogItemService = new CatalogItemService(
                new DbContextWrapper<ApplicationDbContext>(new ApplicationDbContext(null)),
                new Mock<Microsoft.Extensions.Logging.ILogger<BaseDataService<ApplicationDbContext>>>().Object,
                mockCatalogItemRepository.Object,
                mockMapper.Object
            );

    
            var result = await catalogItemService.GetCatalogItemByIdAsync(itemId);


            Assert.IsNotNull(result);
            Assert.AreEqual(mockCatalogItemDto.Id, result.Id);

        }
    }

}
