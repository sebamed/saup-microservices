using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseStatisticsResponseDTO {
        public int average { get; set; }
        public int min { get; set; }
        public int max { get; set; }

    }
}
