using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.ApplicationDbContext;
using Domain.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InstitutesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Institutes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institute>>> GetInstitutes()
        {
            return await _context.Institutes.ToListAsync();
        }

        // GET: api/Institutes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Institute>> GetInstitute(Guid id)
        {
            var institute = await _context.Institutes.FindAsync(id);

            if (institute == null)
            {
                return NotFound();
            }

            return institute;
        }

        // PUT: api/Institutes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitute(Guid id, Institute institute)
        {
            if (id != institute.Id)
            {
                return BadRequest();
            }

            _context.Entry(institute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstituteExists(id))
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

        // POST: api/Institutes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Institute>> PostInstitute(Institute institute)
        {
            _context.Institutes.Add(institute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitute", new { id = institute.Id }, institute);
        }

        // DELETE: api/Institutes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitute(Guid id)
        {
            var institute = await _context.Institutes.FindAsync(id);
            if (institute == null)
            {
                return NotFound();
            }

            _context.Institutes.Remove(institute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstituteExists(Guid id)
        {
            return _context.Institutes.Any(e => e.Id == id);
        }
    }
}
