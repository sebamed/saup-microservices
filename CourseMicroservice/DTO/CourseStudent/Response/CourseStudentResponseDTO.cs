﻿using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseStudentResponseDTO {
        public CourseResponseDTO course { get; set; }
        public StudentResponseDTO student { get; set; }
        public bool activeStudent { get; set; }
        public DateTime beginDate { get; set; }
        public float currentPoints { get; set; }
        public int finalMark { get; set; }
    }
}
