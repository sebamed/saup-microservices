﻿using Commons.DTO;
using System;

namespace CourseMicroservice.DTO.Course {
    public class CourseStudentResponseDTO {
        public string studentUUID { get; set; }
        public bool activeStudent { get; set; }
        public DateTime beginDate { get; set; }
        public float currentPoints { get; set; }
        public int finalMark { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string indexNumber { get; set; }
        public string departmentUUID { get; set; }

    }
}
