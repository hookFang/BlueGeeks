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
    public class PlayerStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlayerStatistics
        public async Task<IActionResult> Index()
        {
            var playerStatisticsContext = _context.PlayerStatistics.Include(p => p.Player_);
            return View(await playerStatisticsContext.ToListAsync());
        }

        // GET: PlayerStatistics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerStatistics = await _context.PlayerStatistics
                .Include(x => x.Player_)
                .FirstOrDefaultAsync(m => m.Player_Statistics_Id == id);
            if (playerStatistics == null)
            {
                return NotFound();
            }

            return View(playerStatistics);
        }

        // GET: PlayerStatistics/Create
        public IActionResult Create()
        {
            ViewData["Player_Id"] = new SelectList(_context.Set<Player>(), "Player_Id", "FirstName");
            return View();
        }

        // POST: PlayerStatistics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Player_Statistics_Id,FgPercent,FtPercent,ThreePointersMade,PointsMade,Rebounds,Assists,Steals,Blocks,TurnOvers,Player_Id")] PlayerStatistics playerStatistics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerStatistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Player_Id"] = new SelectList(_context.Set<Player>(), "Player_Id", "FirstName");
            return View(playerStatistics);
        }

        // GET: PlayerStatistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerStatistics = await _context.PlayerStatistics.FindAsync(id);
            if (playerStatistics == null)
            {
                return NotFound();
            }
            ViewData["Player_Id"] = new SelectList(_context.Set<Player>(), "Player_Id", "FirstName");
            return View(playerStatistics);
        }

        // POST: PlayerStatistics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Player_Statistics_Id,FgPercent,FtPercent,ThreePointersMade,PointsMade,Rebounds,Assists,Steals,Blocks,TurnOvers,Player_Id")] PlayerStatistics playerStatistics)
        {
            if (id != playerStatistics.Player_Statistics_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerStatistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerStatisticsExists(playerStatistics.Player_Statistics_Id))
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
            ViewData["Player_Id"] = new SelectList(_context.Set<Player>(), "Player_Id", "FirstName");
            return View(playerStatistics);
        }

        // GET: PlayerStatistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerStatistics = await _context.PlayerStatistics
                .FirstOrDefaultAsync(m => m.Player_Statistics_Id == id);
            if (playerStatistics == null)
            {
                return NotFound();
            }

            return View(playerStatistics);
        }

        // POST: PlayerStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerStatistics = await _context.PlayerStatistics.FindAsync(id);
            _context.PlayerStatistics.Remove(playerStatistics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerStatisticsExists(int id)
        {
            return _context.PlayerStatistics.Any(e => e.Player_Statistics_Id == id);
        }
    }
}
