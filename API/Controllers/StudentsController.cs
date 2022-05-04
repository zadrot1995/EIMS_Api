using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.ApplicationDbContext;
using Domain.Models;
using Domain.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            student.UserType = Domain.Enums.UserType.Student;
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }
        [HttpPost("test")]
        public async Task<ActionResult<Student>> PostStudentTest(string text)
        {

            return CreatedAtAction("GetStudent", text);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("upload-student-by-excel-file")]
        public async Task<ActionResult<bool>> AddStudentsFromExcel(List<StudentExcelDto> students)
        {
           if(students != null && students.Count != 0)
           {
                foreach(var student in students)
                {
                    student.UniversityName.Trim().ToLower();
                    var university = await _context.Universities
                        .Include(u => u.Institutes)
                            .ThenInclude(i => i.Groups)
                                .ThenInclude(g => g.Students)
                        .Where(u => u.Name.ToLower() == student.UniversityName.Trim().ToLower())
                        .FirstOrDefaultAsync();
                    if(university != null)
                    {
                        var institute = university.Institutes
                            .Where(i => i.Name.ToLower() == student.InstituteName.Trim().ToLower())
                            .FirstOrDefault();
                        if(institute != null)
                        {
                            var group = institute.Groups
                                .Where(group => group.Name.ToLower() == student.GroupName.Trim().ToLower())
                                .FirstOrDefault();
                            if(group != null)
                            {
                                group.Students.Add(new Student
                                {
                                    FirstName = student.FirstName,
                                    SecondName = student.SecondName,
                                    GroupId = group.Id
                                });
                            }
                            else
                            {
                                return BadRequest("There no such group like" + student.GroupName);
                            }
                        }
                        else
                        {
                            return BadRequest("There no such institute like" + student.InstituteName);
                        }
                    }
                    else
                    {
                        return BadRequest("There no such university like" + student.UniversityName);
                    }
                }
                await _context.SaveChangesAsync();
                return Ok();
           }
            else
            {
                return BadRequest("File is empty");
            }
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
