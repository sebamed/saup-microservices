﻿using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMicroservice.Domain.External
{
    public class Course: BaseEntity
    {
        public string name { get; set; }
    }
}
