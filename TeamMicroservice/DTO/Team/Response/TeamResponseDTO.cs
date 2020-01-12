using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.DTO.External;

namespace TeamMicroservice.DTO.Team.Response
{
    public class TeamResponseDTO: BaseDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public TeacherDTO teacher { get; set; }
        public CourseDTO course { get; set; }
    }
}
