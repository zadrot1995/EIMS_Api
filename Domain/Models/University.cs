using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class University
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Institute> Institutes { get; set; }
        public string About { get; set; }
        public List<ImageContent> ImageContents { get; set; }
        public List<Post> Posts { get; set; }
    }
    public class ImageContent
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid UniversityId { get; set; }
        public string ImageName { get; set; }
    }
}
