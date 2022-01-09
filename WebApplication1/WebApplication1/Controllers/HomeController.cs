using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }
        [Route("selfies")]
        [HttpPost]
        public async Task<IActionResult> selfiesAsync([FromForm] IFormCollection postData)
        {
            using var ms = new MemoryStream();
            postData.Files["selfie"].CopyTo(ms);
            var image = ms.ToArray();

            await _db.SAMP.AddAsync(new SAMP
            {
                CreatedAt = DateTime.Now,
                CreatedById = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Image = image,
                Lat = 0,
                Long = 0,
                Location = postData["location"],
                Title = postData["title"],
                UniqueId = postData["id"]
            });
            await _db.SaveChangesAsync();
            return Ok(new { id = postData["id"] });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}