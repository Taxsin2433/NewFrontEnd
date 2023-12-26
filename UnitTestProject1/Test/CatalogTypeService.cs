using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Test
{
    [TestFixture]
    public class CatalogTypeServiceTests
    {
        private Mock<ICatalogTypeRepository> _mockCatalogTypeRepository;
        private Mock<IMapper> _mockMapper;
        private CatalogTypeService _catalogTypeService;

        [SetUp]
        public void Setup()
        {
            _mockCatalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _mockMapper = new Mock<IMapper>();

            _catalogTypeService = new CatalogTypeService(
                new Mock<ILogger<BaseDataService<ApplicationDbContext>>>().Object,
                _mockCatalogTypeRepository.Object,
                _mockMapper.Object
            );
        }

        [Test]
        public async Task GetCatalogTypesAsync_ShouldReturnListOfCatalogTypeDtos()
        {
          
            var mockCatalogTypes = new List<CatalogType> {};
            var mockCatalogTypeDtos = new List<CatalogTypeDto> {};

            _mockCatalogTypeRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(mockCatalogTypes);

            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<CatalogTypeDto>>(mockCatalogTypes))
                .Returns(mockCatalogTypeDtos);

         
            var result = await _catalogTypeService.GetCatalogTypesAsync();

     
            Assert.IsNotNull(result);
            Assert.AreEqual(mockCatalogTypeDtos.Count(), result.Count());

        }

        [Test]
        public async Task AddCatalogTypeAsync_ShouldReturnTypeId()
        {
   
            var request = new CatalogTypeDto {};
            var newTypeId = 789;

            _mockCatalogTypeRepository
                .Setup(repo => repo.AddAsync(request))
                .ReturnsAsync(newTypeId);


            var result = await _catalogTypeService.AddCatalogTypeAsync(request);

   
            Assert.AreEqual(newTypeId, result);
        }

        [Test]
        public async Task DeleteCatalogTypeAsync_ShouldReturnTrueOnSuccessfulDelete()
        {

            var typeId = 101;
            var mockCatalogType = new CatalogType {};

            _mockCatalogTypeRepository
                .Setup(repo => repo.GetByIdAsync(typeId))
                .ReturnsAsync(mockCatalogType);


            var result = await _catalogTypeService.DeleteCatalogTypeAsync(typeId);


            Assert.IsTrue(result);
        }
    }

}
