using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageLink { get; set; }
        [JsonIgnore]
        public University University { get; set; }
        public Guid UniversityId { get; set; }

    }
}
