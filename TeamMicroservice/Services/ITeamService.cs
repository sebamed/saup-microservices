using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.DTO.Team.Request;
using TeamMicroservice.DTO.Team.Response;

namespace TeamMicroservice.Services
{
    public interface ITeamService : ICrudService<MultipleTeamResponseDTO>
    {
        TeamResponseDTO Create(CreateTeamRequestDTO requestDTO);
        TeamResponseDTO Update(UpdateTeamRequestDTO requestDTO);
        TeamResponseDTO Delete(string uuid);
        TeamResponseDTO GetByName(string name);
        TeamResponseDTO GetOneByUuid(string uuid);
    }
}
