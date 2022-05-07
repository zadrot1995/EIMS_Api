using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CreatePostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageLink { get; set; }
        public University University { get; set; }
        public Guid UniversityId { get; set; }
        public IFormFile File { get; set; }
    }
}
