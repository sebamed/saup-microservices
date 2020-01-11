﻿using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseTeacherResponseDTO {
        public string teacherUUID { get; set; }
        public bool activeTeacher { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }

    }
}
