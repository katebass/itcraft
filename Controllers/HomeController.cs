using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Travels.Models;
using Microsoft.EntityFrameworkCore;
using Travels.Data;
using Travels.Models.TravelViewModels;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Travels.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly TravelContext _context;

        public HomeController(TravelContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> About()
        {
            IQueryable<DateGroup> data =
                from tour in _context.Tours
                group tour by tour.Date into dateGroup
                select new DateGroup()
                {
                    Date = dateGroup.Key,
                    ToursCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());

        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
