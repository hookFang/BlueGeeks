using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueGeeks.Data;
using BlueGeeks.Models;

namespace BlueGeeks.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayerStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PlayerStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerStatistics>>> GetPlayerStatistics()
        {
            return await _context.PlayerStatistics.ToListAsync();
        }

        // GET: api/PlayerStatistics/5
        [HttpGet("{id}")]
        public ActionResult<PlayerStatistics> GetPlayerStatistics(int id)
        {
            var playerStatistics = _context.PlayerStatistics.Include(x => x.Player_).Single(p => p.Player_Statistics_Id == id);

            if (playerStatistics == null)
            {
                return NotFound();
            }

            return playerStatistics;
        }

        // PUT: api/PlayerStatistics/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerStatistics(int id, PlayerStatistics playerStatistics)
        {
            if (id != playerStatistics.Player_Statistics_Id)
            {
                return BadRequest();
            }

            _context.Entry(playerStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerStatisticsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlayerStatistics
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PlayerStatistics>> PostPlayerStatistics(PlayerStatistics playerStatistics)
        {
            _context.PlayerStatistics.Add(playerStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerStatistics", new { id = playerStatistics.Player_Statistics_Id }, playerStatistics);
        }

        // DELETE: api/PlayerStatistics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerStatistics>> DeletePlayerStatistics(int id)
        {
            var playerStatistics = await _context.PlayerStatistics.FindAsync(id);
            if (playerStatistics == null)
            {
                return NotFound();
            }

            _context.PlayerStatistics.Remove(playerStatistics);
            await _context.SaveChangesAsync();

            return playerStatistics;
        }

        private bool PlayerStatisticsExists(int id)
        {
            return _context.PlayerStatistics.Any(e => e.Player_Statistics_Id == id);
        }
    }
}
