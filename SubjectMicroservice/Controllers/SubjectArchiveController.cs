using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubjectMicroservice.Localization;
using SubjectMicroservice.Services;
using SubjectMicroservice.DTO.SubjectArchive.Request;
using SubjectMicroservice.DTO.SubjectArchive.Response;
using Commons.Consts;

namespace SubjectMicroservice.Controllers
{
	[Authorize]
	[Route(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BASE)]
	[ApiController]
	public class SubjectArchiveController : ControllerBase
	{
        private readonly ISubjectArchiveService _subjectArchiveService;

        public SubjectArchiveController(ISubjectArchiveService subjectArchiveService) {
            _subjectArchiveService = subjectArchiveService;
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpGet(RouteConsts.ROUTE_ARCHIVES_BY_SUBJECT_UUID)]
        public ActionResult<List<MultipleSubjectArchiveResponseDTO>> HandleGetAllBySubjectUUID(string uuid) {
            return Ok(this._subjectArchiveService.GetAllArchivesBySubjectUUID(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpGet(RouteConsts.ROUTE_LATEST_ARCHIVE_BY_SUBJECT_UUID)]
        public ActionResult<SubjectArchiveResponseDTO> HandleGetLatestbySubjectUUID(string uuid) {
            return Ok(this._subjectArchiveService.GetLatestVersionBySubjectUUID(uuid));
        }
    }
}
