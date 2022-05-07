using API.ApplicationDbContext;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        [HttpGet("university/{universityId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUniversityId(Guid universityId)
        {
            return await _context.Posts.Where(x => x.UniversityId == universityId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostPost()
        {
            var file = this.HttpContext.Request.Form.Files.FirstOrDefault();
            var text = this.HttpContext.Request.Form["text"].ToString();
            var title = this.HttpContext.Request.Form["title"].ToString();
            var universityId = this.HttpContext.Request.Form["universityId"].ToString();

            if (file != null && text != null && title != null && universityId != null)
            {
                var university2 = _context.Universities
                    .Where(x => x.Id.ToString() == universityId)
                    .Include(x => x.Posts);
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
                    Post newPost = new Post
                    {
                        ImageLink = fileName,
                        Text = text,
                        Title = title,
                        UniversityId = new Guid(universityId)
                    };

                    university.Posts.Add(newPost);
                    using (Stream fileStream = new FileStream(uploads, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
