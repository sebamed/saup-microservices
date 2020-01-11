using Commons.Consts;
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

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_TEAM_BASE)]
        public ActionResult<List<MultipleTeamResponseDTO>> HandleGetAll()
        {
            return Ok(this._teamService.GetAll());
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_TEAM_BY_UUID)]
        public ActionResult<TeamResponseDTO> HandleGetOnebyUUID(string uuid)
        {
            return Ok(this._teamService.GetOneByUuid(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_TEAM_BY_NAME)]
        public ActionResult<TeamResponseDTO> HandleGetByName(string name)
        {
            return Ok(this._teamService.GetByName(name));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_TEAM_BY_COURSE)]
        public ActionResult<List<MultipleTeamResponseDTO>> HandleGetByCourse(string uuid)
        {
            return Ok(this._teamService.GetTeamsByCourse(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPost(RouteConsts.ROUTE_TEAM_BASE)]
        public ActionResult<TeamResponseDTO> HandleCreateTeam(CreateTeamRequestDTO requestDTO)
        {
            return Ok(this._teamService.Create(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPut(RouteConsts.ROUTE_TEAM_BASE)]
        public ActionResult<TeamResponseDTO> HandleUpdateTeam(UpdateTeamRequestDTO requestDTO)
        {
            return Ok(this._teamService.Update(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpDelete(RouteConsts.ROUTE_TEAM_BY_UUID)]
        public ActionResult<TeamResponseDTO> HandleDeleteByUUID(string uuid)
        {
            return Ok(this._teamService.Delete(uuid));
        }
    }
}
