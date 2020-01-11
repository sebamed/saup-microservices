using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMicroservice.DTO.Team.Request
{
    public class AddStudentIntoTeamDTO
    {
        [Required]
        public string teamUUID { get; set; }
        [Required]
        public string courseUUID { get; set; }
        [Required]
        public string studentUUID { get; set; }
    }
}
