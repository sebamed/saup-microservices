using Commons.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseMicroservice.Domain {
    public class CourseStudent {
        public Student student { get; set; }
        public Course course { get; set; }
        public bool activeStudent { get; set; }
        public DateTime beginDate { get; set; }
        public float currentPoints { get; set; }
        public int finalMark { get; set; }

    }
}
