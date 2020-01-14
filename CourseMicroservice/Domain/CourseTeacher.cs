using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Domain {
    public class CourseTeacher {
        public Teacher teacher { get; set; }
        public Course course { get; set; }
        public bool activeTeacher { get; set; }
    }
}
