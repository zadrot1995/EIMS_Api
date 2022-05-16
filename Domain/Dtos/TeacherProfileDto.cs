using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class TeacherProfileDto 
    {
        public Teacher Teacher { get; set; }
        public Institute Institute { get; set; }
        public List<Subject> LectureSubjects { get; set; }
        public List<Subject> PracticalSubjects { get; set; }

    }
}
