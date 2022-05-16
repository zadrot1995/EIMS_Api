using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.ApplicationDbContext;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Domain.Dtos;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeachersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(Guid id)
        {
            var file = this.HttpContext.Request.Form.Files.FirstOrDefault();
            var userPhoto = this.HttpContext.Request.Form["UserPhoto"].ToString();
            var firstName = this.HttpContext.Request.Form["firstName"].ToString();
            var secondName = this.HttpContext.Request.Form["secondName"].ToString();
            var education = this.HttpContext.Request.Form["education"].ToString();
            var degree = this.HttpContext.Request.Form["degree"].ToString();
            var about = this.HttpContext.Request.Form["about"].ToString();

            var teacher = await _context.Teachers.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (teacher == null)
            {
                return BadRequest();
            }

            teacher.FirstName = firstName;
            teacher.SecondName = secondName;
            teacher.Degree = degree;
            teacher.About = about;
            teacher.Education = education;


            if (file != null)
            {
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
                    teacher.UserPhoto = fileName;
                    using (Stream fileStream = new FileStream(uploads, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                }


            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            teacher.UserType = Domain.Enums.UserType.Teacher;
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("get-teacher-profile/{id}"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<TeacherProfileDto>> GetStudentProfileDtoAsync(long id)
        {
            var user = _context.LoginModels.Where(x => x.UserName == HttpContext.User.Identity.Name).FirstOrDefault();

            var teacher = await _context.Teachers
                .Include(x => x.Institute)
                .Where(x => x.Id == user.UserDetails)
                .FirstOrDefaultAsync();

            if (teacher != null)
            {
                var subjects = await _context.Subjects
                    .Where(x => x.LecturerId == teacher.Id || x.PractitionerId == teacher.Id)
                    .ToListAsync();

                var teacherProfile = new TeacherProfileDto
                {
                    Teacher = teacher,
                    Institute = teacher.Institute,
                    LectureSubjects = subjects.Where(x => x.LecturerId == teacher.Id).ToList(),
                    PracticalSubjects = subjects.Where(x => x.PractitionerId == teacher.Id).ToList(),
                };
                
                return Ok(teacherProfile);
            }
            else
            {
                return NotFound();
            }
        }
        private bool TeacherExists(Guid id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
