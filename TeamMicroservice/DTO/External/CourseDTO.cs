using Commons.DTO;
using System;

namespace TeamMicroservice.DTO.External
{
    public class CourseDTO: BaseDTO
    {
        public string name { get; set; }

        public string description { get; set; }

        public DateTime creationDate { get; set; }
    }
}
