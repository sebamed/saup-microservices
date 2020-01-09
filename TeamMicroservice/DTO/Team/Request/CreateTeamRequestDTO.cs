using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMicroservice.DTO.Team.Request
{
    public class CreateTeamRequestDTO
    {
        [Required]
        public string uuid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
    }
}
