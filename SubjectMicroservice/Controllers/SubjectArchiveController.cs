using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubjectMicroservice.Localization;
using SubjectMicroservice.Services;
using SubjectMicroservice.DTO.SubjectArchive.Request;
using SubjectMicroservice.DTO.SubjectArchive.Response;

namespace SubjectMicroservice.Controllers
{
	[Authorize]
	[Route(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BASE)]
	[ApiController]
	public class SubjectArchiveController : ControllerBase
	{
        private readonly ISubjectArchiveService _subjectArchiveService;

        public SubjectArchiveController(ISubjectArchiveService subjectArchiveService)
        {
            _subjectArchiveService = subjectArchiveService;
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BASE)]
        public ActionResult<List<SubjectArchiveResponseDTO>> HandleGetAll()
        {
            return Ok(this._subjectArchiveService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BY_UUID)]
        public ActionResult<SubjectArchiveResponseDTO> HandleGetOnebyUUID(string uuid)
        {
            return Ok(this._subjectArchiveService.GetOneByUuid(uuid));
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BY_NAME)]
        public ActionResult<List<SubjectArchiveResponseDTO>> HandleGetByName(string name)
        {
            return Ok(this._subjectArchiveService.GetByName(name));
        }


        [AllowAnonymous]
        [HttpPost(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BASE)]
        public ActionResult<SubjectArchiveResponseDTO> HandleCreateSubjectArchive(CreateSubjectArchiveRequestDTO requestDTO)
        {
            return Ok(this._subjectArchiveService.Create(requestDTO));
        }

        [AllowAnonymous]
        [HttpPut(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BASE)]
        public ActionResult<SubjectArchiveResponseDTO> HandleUpdateSubjectArchive(UpdateSubjectArchiveRequestDTO requestDTO)
        {
            return Ok(this._subjectArchiveService.Update(requestDTO));
        }

        [AllowAnonymous]
        [HttpDelete(RouteConsts.ROUTE_SUBJECT_ARCHIVE_BY_UUID)]
        public ActionResult<SubjectArchiveResponseDTO> HandleDeleteByUUID(string uuid)
        {
            return Ok(this._subjectArchiveService.Delete(uuid));
        }

    }
}
