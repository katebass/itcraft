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
    public class Excursion_SightsController : Controller
    {
        private readonly TravelContext _context;

        public Excursion_SightsController(TravelContext context)
        {
            _context = context;
        }

        // GET: Excursion_Sights
        public async Task<IActionResult> Index()
        {
            return View(await _context.Excursion_Sights.ToListAsync());
        }

        // GET: Excursion_Sights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excursion_Sights = await _context.Excursion_Sights
                .SingleOrDefaultAsync(m => m.ID == id);
            if (excursion_Sights == null)
            {
                return NotFound();
            }

            return View(excursion_Sights);
        }

        // GET: Excursion_Sights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Excursion_Sights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ExcursionName")] Excursion_Sights excursion_Sights)
        {
            if (ModelState.IsValid)
            {
                _context.Add(excursion_Sights);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(excursion_Sights);
        }

        // GET: Excursion_Sights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excursion_Sights = await _context.Excursion_Sights.SingleOrDefaultAsync(m => m.ID == id);
            if (excursion_Sights == null)
            {
                return NotFound();
            }
            return View(excursion_Sights);
        }

        // POST: Excursion_Sights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ExcursionName")] Excursion_Sights excursion_Sights)
        {
            if (id != excursion_Sights.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excursion_Sights);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Excursion_SightsExists(excursion_Sights.ID))
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
            return View(excursion_Sights);
        }

        // GET: Excursion_Sights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excursion_Sights = await _context.Excursion_Sights
                .SingleOrDefaultAsync(m => m.ID == id);
            if (excursion_Sights == null)
            {
                return NotFound();
            }

            return View(excursion_Sights);
        }

        // POST: Excursion_Sights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var excursion_Sights = await _context.Excursion_Sights.SingleOrDefaultAsync(m => m.ID == id);
            _context.Excursion_Sights.Remove(excursion_Sights);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Excursion_SightsExists(int id)
        {
            return _context.Excursion_Sights.Any(e => e.ID == id);
        }
    }
}
