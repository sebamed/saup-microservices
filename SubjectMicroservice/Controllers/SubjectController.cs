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

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_BASE)]
        public ActionResult<List<SubjectResponseDTO>> HandleGetAll()
        {
            return Ok(this._subjectService.GetAll());
        }

		[AllowAnonymous]
		[HttpGet(RouteConsts.ROUTE_SUBJECT_BY_UUID)]
		public ActionResult<SubjectResponseDTO> HandleGetOnebyUUID(string uuid)
		{
			return Ok(this._subjectService.GetOneByUuid(uuid));
		}

		[AllowAnonymous]
		[HttpGet(RouteConsts.ROUTE_SUBJECT_BY_NAME)]
		public ActionResult<List<SubjectResponseDTO>> HandleGetByName(string name)
		{
			return Ok(this._subjectService.GetByName(name));
		}

		[AllowAnonymous]
		[HttpPost(RouteConsts.ROUTE_SUBJECT_BASE)]
		public ActionResult<SubjectResponseDTO> HandleCreateSubject(CreateSubjectRequestDTO requestDTO)
		{
			return Ok(this._subjectService.Create(requestDTO));
		}

		[AllowAnonymous]
		[HttpPut(RouteConsts.ROUTE_SUBJECT_BASE)]
		public ActionResult<SubjectResponseDTO> HandleUpdateSubject(UpdateSubjectRequestDTO requestDTO)
		{
			return Ok(this._subjectService.Update(requestDTO));
		}

		[AllowAnonymous]
		[HttpDelete(RouteConsts.ROUTE_SUBJECT_BY_UUID)]
		public ActionResult<SubjectResponseDTO> HandleDeleteByUUID(string uuid)
		{
			return Ok(this._subjectService.Delete(uuid));
		}

	}
}
