using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.DTO.External;
using TeamMicroservice.DTO.Team.Request;
using TeamMicroservice.DTO.Team.Response;

namespace TeamMicroservice.Services
{
    public interface IStudentTeamService {
        StudentTeamResponseDTO AddStudentIntoTeam(AddStudentIntoTeamDTO requestDTO);
        StudentTeamResponseDTO DeleteStudentFromTeam(string studentUUID,string teamUUID);

    }
}
