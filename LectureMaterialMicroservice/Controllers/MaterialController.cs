using LectureMaterialMicroservice.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SectionMicroservice.DTO.External;
using SectionMicroservice.DTO.Material.Request;
using SectionMicroservice.DTO.Material.Response;
using SectionMicroservice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Controllers
{
    [Authorize]
    [Route(RouteConsts.ROUTE_MATERIAL_BASE)]
    [ApiController]
    public class MaterialController : ControllerBase {

        private readonly IMaterialService _materialService;
        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<MaterialResponseDTO> HandleGetFilesBySection(string sectionUUID)
        {
            return Ok(this._materialService.GetFilesBySection(sectionUUID));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<MaterialResponseDTO> HandleCreateMaterial(CreateMaterialRequestDTO requestDTO)
        {
            // todo: created()
            return Ok(this._materialService.Create(requestDTO));
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult<MaterialResponseDTO> HandleUpdateMaterial(FileDTO requestDTO)
        {
            // todo: created()
            return Ok(this._materialService.UpdateFileInMaterial(requestDTO));
        }

        [AllowAnonymous]
        [HttpDelete]
        public ActionResult<MaterialResponseDTO> HandleDeleteFileBySectionUuid(string sectionUUID, string fileUUID)
        {
            return Ok(this._materialService.Delete(sectionUUID, fileUUID));
        }
    }
}
