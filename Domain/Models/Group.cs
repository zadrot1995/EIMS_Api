using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Curator { get; set; }
        public Guid CuratorId { get; set; }
        [JsonIgnore]
        public Institute Institute { get; set; }
        public Guid InstituteId { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
