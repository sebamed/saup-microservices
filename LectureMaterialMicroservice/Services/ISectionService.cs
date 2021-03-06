﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LectureMaterialMicroservice.Domain;
using LectureMaterialMicroservice.DTO.User;
using SectionMicroservice.DTO.Section.Request;

namespace LectureMaterialMicroservice.Services {
    public interface ISectionService : ICrudService<MultipleSectionResponseDTO> {
        Section FindSectionByUuid(string uuid);
        SectionResponseDTO GetSectionByUuid(string uuid);
        List<MultipleSectionResponseDTO> GetVisibleSections();
        List<MultipleSectionResponseDTO> GetSectionsByCourse(string courseUUID, bool visible);

        SectionResponseDTO Create(CreateSectionRequestDTO requestDTO);
        SectionResponseDTO Update(UpdateSectionRequestDTO requestDTO);
        SectionResponseDTO DeleteSection(string uuid);

    }
}
