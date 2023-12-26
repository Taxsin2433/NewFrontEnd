using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m },
            new Product { Id = 2, Name = "Product 2", Price = 20.49m },
            new Product { Id = 3, Name = "Product 3", Price = 5.99m }
        };

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_products);
        }
    }
}
