using LectureMaterialMicroservice.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public ActionResult<List<SectionArchiveResponseDTO>> HandleGetAllArchives()
        {
            return Ok(this._sectionArchiveService.GetAll());
        }

    }
}
