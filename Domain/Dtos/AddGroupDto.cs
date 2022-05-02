using Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class AddGroupDto
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Curator { get; set; }
        public Guid InstituteId { get; set; }
        public IList AvalibleTeachers { get; set; }
    }
}
