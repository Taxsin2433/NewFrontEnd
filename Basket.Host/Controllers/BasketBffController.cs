using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basket.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketBffController : ControllerBase
    {
        private readonly ILogger<BasketBffController> _logger;

        public BasketBffController(ILogger<BasketBffController> logger)
        {
            _logger = logger;
        }

        [RateLimit]
        [HttpGet("anonymous")]
        [AllowAnonymous]
        public IActionResult AnonymousMethod()
        {
           
            _logger.LogInformation("Anonymous method executed successfully");
            return Ok("Anonymous method executed successfully");
        }

        [RateLimit]
        [HttpGet("defended")]
        public IActionResult DefendedMethod()
        {
        
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation($"Defended method executed successfully. User ID: {userId}");
            return Ok($"Defended method executed successfully. User ID: {userId}");
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class RateLimitAttribute : Attribute { } 
}

