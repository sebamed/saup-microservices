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
    public class StudentTeamController : ControllerBase
    {
        private readonly IStudentTeamService _studentTeamService;

        public StudentTeamController(IStudentTeamService studentTeamService) {
            _studentTeamService = studentTeamService;
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPost(RouteConsts.ROUTE_ADD_STUDENT_INTO_TEAM)]
        public ActionResult<StudentTeamResponseDTO> HandleCreateStudentTeam(AddStudentIntoTeamDTO requestDTO) {
            return Ok(this._studentTeamService.AddStudentIntoTeam(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpDelete(RouteConsts.ROUTE_DELETE_STUDENT_FROM_TEAM)]
        public ActionResult<StudentTeamResponseDTO> HandleDeleteStudentTeam(string teamUUID, string studentUUID)
        {
            return Ok(this._studentTeamService.DeleteStudentFromTeam(studentUUID,teamUUID));
        }
    }
}
