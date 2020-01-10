using Commons.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMicroservice.DTO.Team.Request
{
    public class UpdateTeamRequestDTO: BaseDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string teacherUUID { get; set; }
        [Required]
        public string courseUUID { get; set; }
    }
}
