using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.DTO.External;

namespace TeamMicroservice.DTO.Team.Response
{
    public class StudentTeamResponseDTO
    {
        public TeamResponseDTO team { get; set; }
        public StudentDTO student { get; set; }
    }
}
