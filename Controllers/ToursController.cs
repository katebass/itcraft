using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travels.Data;
using Travels.Models;
using Microsoft.AspNetCore.Authorization;

namespace Travels.Controllers
{
    [Authorize]
    public class ToursController : Controller
    {
        private readonly TravelContext _context;

        public ToursController(TravelContext context)
        {
            _context = context;
        }

        //GET: Tours
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var tours = from s in _context.Tours
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                tours = tours.Where(s => s.TourName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    tours = tours.OrderByDescending(s => s.TourName);
                    break;
                case "Date":
                    tours = tours.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    tours = tours.OrderByDescending(s => s.Date);
                    break;
                default:
                    tours = tours.OrderBy(s => s.TourName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Tours>.CreateAsync(tours.AsNoTracking(), page ?? 1, pageSize));
            //return View(await tours.AsNoTracking().ToListAsync());
        }
        
        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tours = await _context.Tours
                .Include(s => s.Tours_Excursions)
                    .ThenInclude(e => e.Excursion_Sights)
                .AsNoTracking()
                .Include(s => s.Tours_Clients)
                    .ThenInclude(e => e.Clients)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (tours == null)
            {
                return NotFound();
            }

            return View(tours);
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourName,Date")] Tours tours)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tours);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(tours);
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tours = await _context.Tours.SingleOrDefaultAsync(m => m.ID == id);
            if (tours == null)
            {
                return NotFound();
            }
            return View(tours);
        }



        // POST: Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TourName,Date")] Tours tours)
        {
            if (id != tours.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToursExists(tours.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tours);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tours = await _context.Tours
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tours == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(tours);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tours = await _context.Tours
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (tours == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.Tours.Remove(tours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool ToursExists(int id)
        {
            return _context.Tours.Any(e => e.ID == id);
        }
    }
}
