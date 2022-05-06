using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class JournalDto
    {
        public Group Group { get; set; }
        public List<JournalRowDto> JournalRows { get; set; }
        public Subject Subject { get; set; }
    }
}
