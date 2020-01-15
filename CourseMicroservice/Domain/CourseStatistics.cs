using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Domain {
    public class CourseStatistics {
        public int average { get; set; }
        public int min { get; set; }
        public int max { get; set; }
    }
}
