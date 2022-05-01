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
            var test = await _context.Universities.Include(x => x.ImageContents).Include(x => x.Institutes).ToListAsync();
            return test;
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<University>> GetUniversity(Guid id)
        {
            var university = await _context.Universities
                .Include(x => x.ImageContents)
                .Include(x => x.Institutes)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

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

        [HttpDelete("image/delete/{imageId}")]
        public async Task<IActionResult> Get(Guid imageId)
        {
            var university = _context.Universities
                .Include(x => x.ImageContents)
                .Where(x => x.ImageContents
                    .Any(y => y.Id == imageId))
                .FirstOrDefault();
            var image = university.ImageContents.Where(x => x.Id == imageId).FirstOrDefault();

            if (System.IO.File.Exists(image.ImageUrl))
            {
                System.IO.File.Delete(image.ImageUrl);
            }
            university.ImageContents.Remove(image);
            await _context.SaveChangesAsync();
            return Ok();
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
