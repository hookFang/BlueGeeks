using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlueGeeks.Data;
using BlueGeeks.Models;

namespace BlueGeeks.Controllers
{
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coaches.ToListAsync());
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coaches = await _context.Coaches
                .FirstOrDefaultAsync(m => m.Coaches_Id == id);
            if (coaches == null)
            {
                return NotFound();
            }

            return View(coaches);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            ViewData["Team_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Coaches_Id,FirstName,LastName,Title,Team_Id")] Coaches coaches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coaches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            return View(coaches);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coaches = await _context.Coaches.FindAsync(id);
            if (coaches == null)
            {
                return NotFound();
            }
            ViewData["Team_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            return View(coaches);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Coaches_Id,FirstName,LastName,Title,Team_Id")] Coaches coaches)
        {
            if (id != coaches.Coaches_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coaches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachesExists(coaches.Coaches_Id))
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
            ViewData["Team_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            return View(coaches);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coaches = await _context.Coaches
                .FirstOrDefaultAsync(m => m.Coaches_Id == id);
            if (coaches == null)
            {
                return NotFound();
            }

            return View(coaches);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coaches = await _context.Coaches.FindAsync(id);
            _context.Coaches.Remove(coaches);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachesExists(int id)
        {
            return _context.Coaches.Any(e => e.Coaches_Id == id);
        }
    }
}
