using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.Consts;
using TeamMicroservice.DTO;
using TeamMicroservice.DTO.Team.Request;
using TeamMicroservice.DTO.Team.Response;
using TeamMicroservice.Localization;
using TeamMicroservice.Services;

namespace TeamMicroservice.Controllers
{
    [Authorize]
    [Route(RouteConsts.ROUTE_TEAM_BASE)]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_TEAM_BASE)]
        public ActionResult<List<TeamResponseDTO>> HandleGetAll()
        {
            return Ok(this._teamService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_TEAM_BY_UUID)]
        public ActionResult<TeamResponseDTO> HandleGetOnebyUUID(string uuid)
        {
            return Ok(this._teamService.GetOneByUuid(uuid));
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_TEAM_BY_NAME)]
        public ActionResult<List<TeamResponseDTO>> HandleGetByName(string name)
        {
            return Ok(this._teamService.GetByName(name));
        }

        [AllowAnonymous]
        [HttpPost(RouteConsts.ROUTE_TEAM_BASE)]
        public ActionResult<TeamResponseDTO> HandleCreateTeam(CreateTeamRequestDTO requestDTO)
        {
            return Ok(this._teamService.Create(requestDTO));
        }

        [AllowAnonymous]
        [HttpPut(RouteConsts.ROUTE_TEAM_BASE)]
        public ActionResult<TeamResponseDTO> HandleUpdateTeam(UpdateTeamRequestDTO requestDTO)
        {
            return Ok(this._teamService.Update(requestDTO));
        }

        [AllowAnonymous]
        [HttpDelete(RouteConsts.ROUTE_TEAM_BY_UUID)]
        public ActionResult<TeamResponseDTO> HandleDeleteByUUID(string uuid)
        {
            return Ok(this._teamService.Delete(uuid));
        }

    }
}
