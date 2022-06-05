using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Mark
    {
        public Guid Id { get; set; }
        public Student Student { get; set; }
        public Guid StudentId { get; set; }
        public Subject Subject { get; set; }
        public Guid SubjectId { get; set; }
        public MarkType MarkType { get; set; }
        public int Value { get; set; }
        public int Module { get; set; }
    }
}
