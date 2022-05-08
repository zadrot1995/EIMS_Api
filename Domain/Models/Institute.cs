using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Institute
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Group> Groups { get; set; }
        public string About { get; set; }
        public Guid UniversityId { get; set; }
        [JsonIgnore]
        public University University { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<ImageContent> ImageContents { get; set; }


    }
}
