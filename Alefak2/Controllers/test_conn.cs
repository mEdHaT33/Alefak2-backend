using Alefak2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alefak2.Controllers
{
 
        [ApiController]
        [Route("[controller]")]
        public class TestController : ControllerBase
        {
            private readonly ApiContext _context;

            public TestController(ApiContext context)
            {
                _context = context;
            }

            [HttpGet("ping")]
            public IActionResult Ping()
            {
                try
                {
                    var canConnect = _context.Database.CanConnect();
                    return Ok(canConnect ? "✅ Conn successful!" : "❌ Failed to conn.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"❗ Error: {ex.Message}");
                }
            }
        }
}

