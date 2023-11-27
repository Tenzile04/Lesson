using Microsoft.AspNetCore.Mvc;
using PustokBookCrud.ViewModels;
using PustokBookCrud.DAL;


namespace PustokBookCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
                _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Sliders = _context.Sliders.ToList(),
                Services = _context.Services.ToList()
            };

            return View(homeViewModel);
        }
    }
}
