﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Student : Member 
    {        
        public Guid GroupId { get; set; }
    }
}
