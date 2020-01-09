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

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<SectionArchiveResponseDTO> HandleCreateSection(CreateSectionArchiveRequestDTO requestDTO)
        {
            // todo: created()
            return Ok(this._sectionArchiveService.Create(requestDTO));
        }

        [AllowAnonymous]
        [HttpGet(RouteConsts.ROUTE_SECTION_GET_ARCHIVE_BY_SECTION_UUID)]
        public ActionResult<SectionArchiveResponseDTO> HandleGetOneArchiveBySectionUUID(string sectionUUID)
        {
            return Ok(this._sectionArchiveService.GetOneByArchiveBySectionUuid(sectionUUID));
        }
    }
}
