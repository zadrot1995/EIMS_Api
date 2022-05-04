using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class JournalRowDto
    {
        public Student Student { get; set; }
        public List<Mark> Marks { get; set; }
    }
}
