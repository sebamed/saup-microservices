using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseStudentResponseDTO {
        public string studentUUID { get; set; }
        public bool activeStudent { get; set; }
        public DateTime beginDate { get; set; }
        public float currentPoints { get; set; }
        public int finalMark { get; set; }

    }
}
