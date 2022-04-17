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
    public class UniversitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UniversitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Universities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<University>>> GetUniversities()
        {
            return await _context.Universities.Include(x => x.ImageContents).ToListAsync();
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<University>> GetUniversity(Guid id)
        {
            var university = await _context.Universities.FindAsync(id);

            if (university == null)
            {
                return NotFound();
            }

            return university;
        }

        // PUT: api/Universities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUniversity(Guid id, University university)
        {
            if (id != university.Id)
            {
                return BadRequest();
            }

            _context.Entry(university).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityExists(id))
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

        // POST: api/Universities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<University>> PostUniversity(University university)
        {
            _context.Universities.Add(university);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUniversity", new { id = university.Id }, university);
        }

        // DELETE: api/Universities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(Guid id)
        {
            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("addimage")]
        public async Task<IActionResult> AddImage(IFormFile image)
        {
            return Ok();

        }

        [HttpGet("image/{imageName}")]
        public IActionResult Get(string imageName)
        {
            var image = System.IO.File.OpenRead($"C:\\Users\\zadro\\OneDrive\\Documents\\uploads\\{imageName}");
            return File(image, "image/jpeg");
        }


        [HttpPost("image/{id}")]
        public async Task<ActionResult> Imager(IFormFile file, Guid id)
        {
            {
                if (file != null)
                {
                    var university2 = _context.Universities
                        .Where(x => x.Id == id)
                        .Include(x => x.ImageContents);
                    var university = university2.FirstOrDefault();


                    string uploads = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uploads");
                    uploads = Path.Combine(uploads, DateTime.Now.Ticks.ToString() + file.FileName).Replace(" ", "");
                    //uploads = uploads.Replace(".", "");
                    //uploads = uploads.Replace(":", "");
                    //uploads = Path.Combine(uploads, file.FileName).Replace(" ", "");

                    var imageUrl = uploads;

                    if (file.Length > 0)
                    {
                        ImageContent fileContent = new ImageContent
                        {
                            ImageUrl = imageUrl,
                            ImageName = file.FileName
                        };

                        university.ImageContents.Add(fileContent);
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


        private bool UniversityExists(Guid id)
        {
            return _context.Universities.Any(e => e.Id == id);
        }
    }
}
