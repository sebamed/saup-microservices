using Commons.Consts;
using FileMicroservice.DTO;
using FileMicroservice.DTO.File;
using FileMicroservice.DTO.File.Request;
using FileMicroservice.Localization;
using FileMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace FileMicroservice.Controllers
{
    [Authorize]
    [Route(RouteConsts.ROUTE_FILE_BASE)]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly IFileService _fileService;

        public FileController(IFileService fileService) {
            _fileService = fileService;
        }

        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpGet(RouteConsts.ROUTE_FILE_BASE)]
        public ActionResult<List<FileResponseDTO>> HandleGetAllFiles() {
            return Ok(this._fileService.GetAll());
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_FILE_GET_ONE_BY_UUID)]
        public ActionResult<FileResponseDTO> HandleGetOneFileByUuid(string uuid) {
            return Ok(this._fileService.GetOneByUuid(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_FILE_BY_PATH)]
        public ActionResult HandleGetFileByPath(string path)
        {
            return File(
                this._fileService.GetFileByPath(path), 
                "application/octet-stream", 
                "file"+ Path.GetExtension(path)
                );
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPost(RouteConsts.ROUTE_FILE_BASE)]
        public ActionResult<FileResponseDTO> HandleCreateFile(CreateFileRequestDTO requestDTO)
        {
            return Ok(this._fileService.Create(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpPut(RouteConsts.ROUTE_FILE_BASE)]
        public ActionResult<FileResponseDTO> HandleUpdateFile(UpdateFileRequestDTO requestDTO)
        {
            return Ok(this._fileService.Update(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)] 
        [HttpDelete(RouteConsts.ROUTE_FILE_BASE)]
        public ActionResult<FileResponseDTO> HandleDeleteByUUID(string uuid)
        {
            return Ok(this._fileService.Delete(uuid));
        }

    }
}