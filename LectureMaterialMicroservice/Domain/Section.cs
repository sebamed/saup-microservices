﻿using Commons.Domain;
using SectionMicroservice.Domain.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Domain{
    public class Section : BaseEntity {

        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public int visible { get; set; }

        public DateTime creationDate { get; set; }

        public Course course { get; set; }
    }
}
