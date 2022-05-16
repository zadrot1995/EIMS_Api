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
using Microsoft.AspNetCore.Authorization;
using System.IO;

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
        public async Task<IActionResult> PutStudent(Guid id)
        {
            var file = this.HttpContext.Request.Form.Files.FirstOrDefault();
            var userPhoto = this.HttpContext.Request.Form["UserPhoto"].ToString();
            var firstName = this.HttpContext.Request.Form["firstName"].ToString();
            var secondName = this.HttpContext.Request.Form["secondName"].ToString();

            var student = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (student == null)
            {
                return BadRequest();
            }

            student.FirstName = firstName;
            student.SecondName = secondName;

            if(file != null)
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
                        student.UserPhoto = fileName;
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

        [HttpGet("get-student-profile/{id}"), Authorize(Roles = "Student")]
        public async Task<ActionResult<StudentProfileDto>> GetStudentProfileDtoAsync(long id)
        {
            var user = _context.LoginModels.Where(x => x.UserName == HttpContext.User.Identity.Name).FirstOrDefault();

            var student = await _context.Students.Where(x => x.Id == user.UserDetails).FirstOrDefaultAsync();

            if(student != null)
            {
                var studentProfile = new StudentProfileDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    UserPhoto = student.UserPhoto,
                    UserJournal = new UserJournal()
                };
                var group = await _context.Groups
                    .Include(x => x.Subjects)
                    .Include(x => x.Institute)
                    .ThenInclude(y => y.University)
                    .Where(x => x.Id == student.GroupId).FirstOrDefaultAsync();

                studentProfile.Group = group;
                studentProfile.Institute = group.Institute;
                studentProfile.University = group.Institute.University;
                studentProfile.UserJournal = new UserJournal();
                studentProfile.UserJournal.UserJournalRows = new List<UserJournalRow>();
                foreach(var subject in group.Subjects)
                {
                    subject.Groups = null;
                    var userJournalRow = new UserJournalRow()
                    {
                        SubjectName = subject.Name,
                        Marks = await _context.Marks
                        .Where(x => x.StudentId == student.Id && x.SubjectId == subject.Id)
                        .ToListAsync()
                    };
                    studentProfile.UserJournal.UserJournalRows.Add(userJournalRow);
                }
                return Ok(studentProfile);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("student-overview/{id}")]
        public async Task<ActionResult<StudentProfileDto>> GetStudentOverviewAsync(Guid id)
        {

            var student = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (student != null)
            {
                var studentProfile = new StudentProfileDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    UserPhoto = student.UserPhoto,
                    UserJournal = new UserJournal()
                };
                var group = await _context.Groups
                    .Include(x => x.Subjects)
                    .Include(x => x.Institute)
                    .ThenInclude(y => y.University)
                    .Where(x => x.Id == student.GroupId).FirstOrDefaultAsync();

                studentProfile.Group = group;
                studentProfile.Institute = group.Institute;
                studentProfile.University = group.Institute.University;
                studentProfile.UserJournal = new UserJournal();
                studentProfile.UserJournal.UserJournalRows = new List<UserJournalRow>();
                foreach (var subject in group.Subjects)
                {
                    subject.Groups = null;
                    var userJournalRow = new UserJournalRow()
                    {
                        SubjectName = subject.Name,
                        Marks = await _context.Marks
                        .Where(x => x.StudentId == student.Id && x.SubjectId == subject.Id)
                        .ToListAsync()
                    };
                    studentProfile.UserJournal.UserJournalRows.Add(userJournalRow);
                }
                return Ok(studentProfile);
            }
            else
            {
                return NotFound();
            }
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
