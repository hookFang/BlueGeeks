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
    public class CoachesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Coaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coaches>>> GetCoaches()
        {
            return await _context.Coaches.ToListAsync();
        }

        // GET: api/Coaches/5
        [HttpGet("{id}")]
        public ActionResult<Coaches> GetCoaches(int id)
        {
            var coaches = _context.Coaches.Include(x => x.Team_).Single(p => p.Coaches_Id == id);

            if (coaches == null)
            {
                return NotFound();
            }

            return coaches;
        }

        // PUT: api/Coaches/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoaches(int id, Coaches coaches)
        {
            if (id != coaches.Coaches_Id)
            {
                return BadRequest();
            }

            _context.Entry(coaches).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachesExists(id))
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

        // POST: api/Coaches
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Coaches>> PostCoaches(Coaches coaches)
        {
            _context.Coaches.Add(coaches);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoaches", new { id = coaches.Coaches_Id }, coaches);
        }

        // DELETE: api/Coaches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Coaches>> DeleteCoaches(int id)
        {
            var coaches = await _context.Coaches.FindAsync(id);
            if (coaches == null)
            {
                return NotFound();
            }

            _context.Coaches.Remove(coaches);
            await _context.SaveChangesAsync();

            return coaches;
        }

        private bool CoachesExists(int id)
        {
            return _context.Coaches.Any(e => e.Coaches_Id == id);
        }
    }
}
