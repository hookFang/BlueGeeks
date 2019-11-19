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
    public class StadiumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StadiumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stadiums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stadium.ToListAsync());
        }

        // GET: Stadiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium
                .FirstOrDefaultAsync(m => m.Stadium_Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // GET: Stadiums/Create
        public IActionResult Create()
        {
            ViewData["Team_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            return View();
        }

        // POST: Stadiums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stadium_Id,StadiumName,City,Team_Id")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stadium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Team_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            return View(stadium);
        }

        // GET: Stadiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }
            ViewData["Team_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            return View(stadium);
        }

        // POST: Stadiums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Stadium_Id,StadiumName,City,Team_Id")] Stadium stadium)
        {
            if (id != stadium.Stadium_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stadium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StadiumExists(stadium.Stadium_Id))
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
            return View(stadium);
        }

        // GET: Stadiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadium
                .FirstOrDefaultAsync(m => m.Stadium_Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // POST: Stadiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadium = await _context.Stadium.FindAsync(id);
            _context.Stadium.Remove(stadium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StadiumExists(int id)
        {
            return _context.Stadium.Any(e => e.Stadium_Id == id);
        }
    }
}
