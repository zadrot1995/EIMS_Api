using API.ApplicationDbContext;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Marks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mark>>> GetMarks()
        {
            return await _context.Marks.ToListAsync();
        }

        // GET: api/Marks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mark>> GetMark(Guid id)
        {
            var mark = await _context.Marks
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (mark == null)
            {
                return NotFound();
            }

            return mark;
        }

        // PUT: api/Marks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMark(Guid id, Mark mark)
        {
            if (id != mark.Id)
            {
                return BadRequest();
            }

            _context.Entry(mark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarkExists(id))
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

        // POST: api/Marks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostMark(Mark mark)
        {
            _context.Marks.Add(mark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMark", new { id = mark.Id }, mark);
        }

        // DELETE: api/Marks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMark(Guid id)
        {
            var mark = await _context.Marks.FindAsync(id);
            if (mark == null)
            {
                return NotFound();
            }

            _context.Marks.Remove(mark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("getMarks/{subjectId}/{groupId}")]
        public async Task<ActionResult<JournalDto>> GetMarks(Guid subjectId, Guid groupId)
        {
            JournalDto journal = new JournalDto();
            var group = _context.Groups
                .Include(x => x.Students)
                .Where(x => x.Id == groupId)
                .FirstOrDefault();
            if(group != null)
            {
                journal.Group = group;
                journal.JournalRows = new List<JournalRowDto>();
                foreach(var student in journal.Group.Students)
                {


                    JournalRowDto journalRow = new JournalRowDto
                    {
                        Student = student,
                        Marks = await _context.Marks
                            .Where(x => x.StudentId == student.Id && x.SubjectId == subjectId)
                            .ToListAsync()
                    };
                    journal.JournalRows.Add(journalRow);
                }
                return Ok(journal);
            }
            else
            {
                return NotFound("Group not found");
            }           
        }


        private bool MarkExists(Guid id)
        {
            return _context.Marks.Any(e => e.Id == id);
        }
    }
}
