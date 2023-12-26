using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestFixture]
    public class CatalogBrandServiceTests
    {
        private Mock<ICatalogBrandRepository> _mockCatalogBrandRepository;
        private Mock<IMapper> _mockMapper;
        private CatalogBrandService _catalogBrandService;

        [SetUp]
        public void Setup()
        {
            _mockCatalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _mockMapper = new Mock<IMapper>();

            _catalogBrandService = new CatalogBrandService(
                new Mock<ILogger<BaseDataService<ApplicationDbContext>>>().Object,
                _mockCatalogBrandRepository.Object,
                _mockMapper.Object
            );
        }

        [Test]
        public async Task GetCatalogBrandsAsync_ShouldReturnListOfCatalogBrandDtos()
        {
 
            var mockCatalogBrands = new List<CatalogBrand> {};
            var mockCatalogBrandDtos = new List<CatalogBrandDto> {};

            _mockCatalogBrandRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(mockCatalogBrands);

            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<CatalogBrandDto>>(mockCatalogBrands))
                .Returns(mockCatalogBrandDtos);


            var result = await _catalogBrandService.GetCatalogBrandsAsync();


            Assert.IsNotNull(result);
            Assert.AreEqual(mockCatalogBrandDtos.Count(), result.Count());

        }

        [Test]
        public async Task AddCatalogBrandAsync_ShouldReturnBrandId()
        {
         
            var request = new CatalogBrandDto { };
            var newBrandId = 456;

            _mockCatalogBrandRepository
                .Setup(repo => repo.AddAsync(request))
                .ReturnsAsync(newBrandId);

          
            var result = await _catalogBrandService.AddCatalogBrandAsync(request);

           
            Assert.AreEqual(newBrandId, result);
        }

        [Test]
        public async Task DeleteCatalogBrandAsync_ShouldReturnTrueOnSuccessfulDelete()
        {
           
            var brandId = 789;
            var mockCatalogBrand = new CatalogBrand {};

            _mockCatalogBrandRepository
                .Setup(repo => repo.GetByIdAsync(brandId))
                .ReturnsAsync(mockCatalogBrand);

           
            var result = await _catalogBrandService.DeleteCatalogBrandAsync(brandId);

           
            Assert.IsTrue(result);
           
        }
    }

}
