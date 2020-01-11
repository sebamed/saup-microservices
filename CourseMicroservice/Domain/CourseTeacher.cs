using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Domain {
    public class CourseTeacher {
        public string teacherUUID { get; set; }
        public string courseUUID { get; set; }
        public bool activeTeacher { get; set; }
    }
}
