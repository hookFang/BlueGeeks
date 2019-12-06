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
    public class StadiumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StadiumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Stadium
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stadium>>> GetStadium()
        {
            return await _context.Stadium.ToListAsync();
        }

        // GET: api/Stadium/5
        [HttpGet("{id}")]
        public ActionResult<Stadium> GetStadium(int id)
        {
            var stadium = _context.Stadium.Include(x => x.Team_).Single(p => p.Stadium_Id == id);

            if (stadium == null)
            {
                return NotFound();
            }

            return stadium;
        }

        // PUT: api/Stadium/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStadium(int id, Stadium stadium)
        {
            if (id != stadium.Stadium_Id)
            {
                return BadRequest();
            }

            _context.Entry(stadium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StadiumExists(id))
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

        // POST: api/Stadium
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Stadium>> PostStadium(Stadium stadium)
        {
            _context.Stadium.Add(stadium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStadium", new { id = stadium.Stadium_Id }, stadium);
        }

        // DELETE: api/Stadium/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stadium>> DeleteStadium(int id)
        {
            var stadium = await _context.Stadium.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }

            _context.Stadium.Remove(stadium);
            await _context.SaveChangesAsync();

            return stadium;
        }

        private bool StadiumExists(int id)
        {
            return _context.Stadium.Any(e => e.Stadium_Id == id);
        }
    }
}
