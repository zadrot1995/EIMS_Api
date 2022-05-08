using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.ApplicationDbContext;
using Domain.Models;
using System.IO;

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
            return await _context.Institutes
                .Include(x => x.University)
                .Include(x => x.ImageContents)
                .ToListAsync();
        }

        // GET: api/Institutes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Institute>> GetInstitute(Guid id)
        {
            var institute = await _context.Institutes
                .Include(x => x.Teachers)
                .Include(x => x.Groups)
                .Include(x => x.Subjects)
                .Include(x => x.ImageContents)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

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
        [HttpGet("teachers/{instituteId}")]
        public async Task<ActionResult<IList<Teacher>>> GetTeachersByInstituteId(Guid instituteId)
        {
            var institute = await _context.Institutes
                .Include(x => x.Teachers)
                .Where(x => x.Id == instituteId)
                .FirstOrDefaultAsync();
            if(institute != null)
            {
                return institute.Teachers;
            }
            else
            {
                return NotFound();
            }


        }

        [HttpGet("university/{universityId}")]
        public async Task<ActionResult<IList<Institute>>> GetInstitutesByUniversityId(Guid universityId)
        {
            var university = await _context.Universities
                .Include(x => x.Institutes)
                .ThenInclude(y => y.ImageContents)
                .Where(x => x.Id == universityId)
                .FirstOrDefaultAsync();
            if (university != null)
            {
                return university.Institutes;
            }
            else
            {
                return NotFound();
            }


        }



        [HttpPost("image/{id}")]
        public async Task<ActionResult> Imager(IFormFile file, Guid id)
        {
            {
                if (file != null)
                {
                    var instituteQwery = _context.Institutes
                        .Where(x => x.Id == id)
                        .Include(x => x.ImageContents);
                    var institute = instituteQwery.FirstOrDefault();

                    string uploads = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uploads");
                    var fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                    uploads = Path.Combine(uploads, fileName).Replace(" ", "");
                    //uploads = uploads.Replace(".", "");
                    //uploads = uploads.Replace(":", "");
                    //uploads = Path.Combine(uploads, file.FileName).Replace(" ", "");

                    var imageUrl = uploads;

                    if (file.Length > 0)
                    {
                        ImageContent fileContent = new ImageContent
                        {
                            ImageUrl = imageUrl,
                            ImageName = fileName
                        };

                        institute.ImageContents.Add(fileContent);
                        using (Stream fileStream = new FileStream(fileContent.ImageUrl, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                    }

                    await _context.SaveChangesAsync();
                    return Ok(file);
                }
                else
                {
                    return BadRequest(file);
                }

            }
        }



        private bool InstituteExists(Guid id)
        {
            return _context.Institutes.Any(e => e.Id == id);
        }
    }
}
