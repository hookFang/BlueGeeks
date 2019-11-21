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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var matchesContext = _context.Matches.Include(p => p.HomeTeam_).Include(x => x.AwayTeam_).Include(c => c.Stadium_);
            return View(await matchesContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Matches
                .Include(p => p.HomeTeam_)
                .Include(x => x.AwayTeam_)
                .Include(c => c.Stadium_)
                .FirstOrDefaultAsync(m => m.Matche_Id == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["HomeTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["AwayTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["Stadium_Id"] = new SelectList(_context.Set<Stadium>(), "Stadium_Id", "StadiumName");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matche_Id,HomeTeam_Id,AwayTeam_Id,MatchDate,Stadium_Id")] Matches matches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matches);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HomeTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["AwayTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["Stadium_Id"] = new SelectList(_context.Set<Stadium>(), "Stadium_Id", "StadiumName");
            return View(matches);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Matches.FindAsync(id);
            if (matches == null)
            {
                return NotFound();
            }
            ViewData["HomeTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["AwayTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["Stadium_Id"] = new SelectList(_context.Set<Stadium>(), "Stadium_Id", "StadiumName");
            return View(matches);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matche_Id,HomeTeam_Id,AwayTeam_Id,MatchDate,Stadium_Id")] Matches matches)
        {
            if (id != matches.Matche_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchesExists(matches.Matche_Id))
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
            ViewData["HomeTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["AwayTeam_Id"] = new SelectList(_context.Set<Teams>(), "Team_Id", "Team_Name");
            ViewData["Stadium_Id"] = new SelectList(_context.Set<Stadium>(), "Stadium_Id", "StadiumName");
            return View(matches);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matches = await _context.Matches
                .FirstOrDefaultAsync(m => m.Matche_Id == id);
            if (matches == null)
            {
                return NotFound();
            }

            return View(matches);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matches = await _context.Matches.FindAsync(id);
            _context.Matches.Remove(matches);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchesExists(int id)
        {
            return _context.Matches.Any(e => e.Matche_Id == id);
        }
    }
}
