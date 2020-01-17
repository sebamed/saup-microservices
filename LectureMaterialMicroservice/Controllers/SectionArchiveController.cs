using Commons.Consts;
using LectureMaterialMicroservice.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SectionMicroservice.DTO.SectionArchive.Request;
using SectionMicroservice.DTO.SectionArchive.Response;
using SectionMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Controllers {
    [Authorize]
    [Route(RouteConsts.ROUTE_SECTION_ARCHIVE_BASE)]
    [ApiController]
    public class SectionArchiveController : ControllerBase {

        private readonly ISectionArchiveService _sectionArchiveService;

        public SectionArchiveController(ISectionArchiveService sectionArchiveService) {
            _sectionArchiveService = sectionArchiveService;
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpGet(RouteConsts.ROUTE_ARCHIVES_BY_SECTION_UUID)]
        public ActionResult<MultipleSectionArchiveResponseDTO> HandleGetAllArchivesBySectionUUID(string uuid)
        {
            return Ok(this._sectionArchiveService.GetAllArchivesBySectionUUID(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpGet(RouteConsts.ROUTE_LATEST_ARCHIVE_BY_SECTION_UUID)]
        public ActionResult<SectionArchiveResponseDTO> HandleGetLatestbySectionUUID(string uuid)
        {
            return Ok(this._sectionArchiveService.GetLatestVersionBySectionoUUID(uuid));
        }
    }
}
