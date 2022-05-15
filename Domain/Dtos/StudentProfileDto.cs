using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class StudentProfileDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<SubjectDto> Subjects { get; set; }
        public Group Group { get; set; }
        public Institute Institute { get; set; }
        public University University { get; set; }
        public UserJournal UserJournal { get; set; }
        public string UserPhoto { get; set; }

    }
}
