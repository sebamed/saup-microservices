using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.DTO.External;
using TeamMicroservice.DTO.Team.Request;
using TeamMicroservice.DTO.Team.Response;

namespace TeamMicroservice.Services
{
    public interface ITeamService : ICrudService<MultipleTeamResponseDTO>
    {
        TeamResponseDTO Create(CreateTeamRequestDTO requestDTO);
        TeamResponseDTO Update(UpdateTeamRequestDTO requestDTO);
        TeamResponseDTO Delete(string uuid);
        TeamResponseDTO GetOneByUuid(string uuid);
        List<MultipleTeamResponseDTO> GetByName(string name);
        List<MultipleTeamResponseDTO> GetTeamsByCourse(string courseUUID);
    }
}
