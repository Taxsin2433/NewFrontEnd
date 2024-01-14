using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketApiController : ControllerBase
    {
        [RateLimit]
        public Task<IActionResult> AddItem() 
        {
            return Ok(result);
        }
    }
}
