using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubjectMicroservice.Consts;
using SubjectMicroservice.Localization;
using SubjectMicroservice.Services;
using Commons.Consts;
using SubjectMicroservice.DTO.Subject;
using SubjectMicroservice.DTO.Subject.Request;
using SubjectMicroservice.DTO.Subject.Response;

namespace SubjectMicroservice.Controllers
{
	[Authorize]
	[Route(RouteConsts.ROUTE_SUBJECT_BASE)]
	[ApiController]
	public class SubjectController : ControllerBase
	{
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_BASE)]
        public ActionResult<List<SubjectResponseDTO>> HandleGetAll()
        {
            return Ok(this._subjectService.GetAll());
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_BY_UUID)]
		public ActionResult<SubjectResponseDTO> HandleGetOnebyUUID(string uuid)
		{
			return Ok(this._subjectService.GetOneByUuid(uuid));
		}

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_BY_NAME)]
		public ActionResult<List<SubjectResponseDTO>> HandleGetByName(string name)
		{
			return Ok(this._subjectService.GetByName(name));
		}

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_BY_DEPARTMENT_UUID)]
        public ActionResult<List<SubjectResponseDTO>> HandleGetByDepartmentUUID(string uuid)
        {
            return Ok(this._subjectService.GetByDepartmentUUID(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_BY_CREATOR_UUID)]
        public ActionResult<List<SubjectResponseDTO>> HandleGetByCreatorUUID(string uuid)
        {
            return Ok(this._subjectService.GetByCreatorUUID(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPost(RouteConsts.ROUTE_SUBJECT_BASE)]
		public ActionResult<SubjectResponseDTO> HandleCreateSubject(CreateSubjectRequestDTO requestDTO)
		{
			return Ok(this._subjectService.Create(requestDTO));
		}

        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPut(RouteConsts.ROUTE_SUBJECT_BASE)]
		public ActionResult<SubjectResponseDTO> HandleUpdateSubject(UpdateSubjectRequestDTO requestDTO)
		{
			return Ok(this._subjectService.Update(requestDTO));
		}

        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpDelete(RouteConsts.ROUTE_SUBJECT_BY_UUID)]
		public ActionResult<SubjectResponseDTO> HandleDeleteByUUID(string uuid)
		{
			return Ok(this._subjectService.Delete(uuid));
		}

	}
}
