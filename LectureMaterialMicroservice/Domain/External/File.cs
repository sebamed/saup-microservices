﻿using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Domain.External
{
    public class File : BaseEntity {
        public string path { get; set; }
    }
}
