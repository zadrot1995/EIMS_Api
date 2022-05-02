using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Teacher Lecturer { get; set; }
        public Guid LecturerId { get; set; }
        public Teacher Practitioner { get; set; }
        public Guid PractitionerId { get; set; }
        public List<Group> Groups { get; set; }
        [JsonIgnore]
        public Institute Institute { get; set; }
        public Guid InstituteId { get; set; }
    }
}
