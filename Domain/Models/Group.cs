﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public List<Student> Students { get; set; }
        public Teacher Curator { get; set; }
    }
}
