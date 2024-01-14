using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("anonymous")]
        [AllowAnonymous]
        public IActionResult AnonymousMethod()
        {
            // Log any test message for anonymous access
            _logger.LogInformation("Anonymous method executed successfully");
            return Ok("Anonymous method executed successfully");
        }

        [HttpGet("defended")]
        public IActionResult DefendedMethod()
        {
            // Read "user id" from the incoming request and log it
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Log the user ID
            _logger.LogInformation($"Defended method executed successfully. User ID: {userId}");

            return Ok($"Defended method executed successfully. User ID: {userId}");
        }
    }
}
    

