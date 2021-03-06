﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LectureMaterialMicroservice.Consts;
using LectureMaterialMicroservice.DTO.User;
using LectureMaterialMicroservice.Localization;
using LectureMaterialMicroservice.Services;
using Commons.Consts;
using SectionMicroservice.DTO.Section.Request;
using LectureMaterialMicroservice.Domain;

namespace LectureMaterialMicroservice.Controllers {
    [Authorize]
    [Route(RouteConsts.ROUTE_SECTION_BASE)]
    [ApiController]
    public class SectionController : ControllerBase {

        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService) {
            _sectionService = sectionService;
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpGet]
        public ActionResult<List<MultipleSectionResponseDTO>> HandleGetAllSections() {
            return Ok(this._sectionService.GetAll());
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_SECTION_GET_VISIBLE)]
        public ActionResult<List<MultipleSectionResponseDTO>> HandleVisibleSections()
        {
            return Ok(this._sectionService.GetVisibleSections());
        }

        [Authorize(Roles = RoleConsts.ROLE_USER)]
        [HttpGet(RouteConsts.ROUTE_SECTION_GET_VISIBLE_BY_COURSE)]
        public ActionResult<List<MultipleSectionResponseDTO>> HandleVisibleSectionsByCourse(string uuid) {
            return Ok(this._sectionService.GetSectionsByCourse(uuid, true));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpGet(RouteConsts.ROUTE_SECTION_GET_BY_COURSE)]
        public ActionResult<List<MultipleSectionResponseDTO>> HandleSectionsByCourse(string uuid) {
            return Ok(this._sectionService.GetSectionsByCourse(uuid,false));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpGet(RouteConsts.ROUTE_SECTION_GET_ONE_BY_UUID)]
        public ActionResult<SectionResponseDTO> HandleGetOneSectionByUuid(string uuid)
        {
            return Ok(this._sectionService.GetSectionByUuid(uuid));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPost]
        public ActionResult<SectionResponseDTO> HandleCreateSection(CreateSectionRequestDTO requestDTO) {
            // todo: created()
            return Ok(this._sectionService.Create(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [HttpPut]
        public ActionResult<SectionResponseDTO> HandleUpdateSection(UpdateSectionRequestDTO requestDTO)
        {
            return Ok(this._sectionService.Update(requestDTO));
        }

        [Authorize(Roles = RoleConsts.ROLE_TEACHER)]
        [Authorize(Roles = RoleConsts.ROLE_ADMIN)]
        [HttpDelete]
        public ActionResult<SectionResponseDTO> HandleDeleteSectionByUuid(string uuid)
        {
            return Ok(this._sectionService.DeleteSection(uuid));
        }
    }
}