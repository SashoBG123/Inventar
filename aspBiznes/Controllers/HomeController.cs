using System.Diagnostics;
using aspBiznes.Data;
using aspBiznes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspBiznes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Reports()
        {
            var model = new ReportsViewModel
            {
                ItemsCount = await _context.Items.CountAsync(),
                CategoriesCount = await _context.Categories.CountAsync(),
                SuppliersCount = await _context.Suppliers.CountAsync(),
                OrdersCount = await _context.Carts.CountAsync(),
                TotalStock = await _context.Items.SumAsync(item => (int?)item.Quantity) ?? 0,
                LowStockItems = await _context.Items.CountAsync(item => item.Quantity > 0 && item.Quantity <= 5),
                OutOfStockItems = await _context.Items.CountAsync(item => item.Quantity == 0)
            };

            return View(model);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
