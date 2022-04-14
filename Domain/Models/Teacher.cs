using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Teacher : Member
    {
        public string Degree { get; set; }
        public string Education { get; set; }
        public string About { get; set; }

    }
}
