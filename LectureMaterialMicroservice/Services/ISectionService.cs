﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LectureMaterialMicroservice.Domain;
using LectureMaterialMicroservice.DTO.User;
using SectionMicroservice.DTO.Section.Request;

namespace LectureMaterialMicroservice.Services {
    public interface ISectionService : ICrudService<SectionResponseDTO> {

        SectionResponseDTO Create(CreateSectionRequestDTO requestDTO);

        //SectionResponseDTO Update(UpdateUserRequestDTO requestDTO);
        Section FindOneByUuidOrThrow(string uuid);
        SectionResponseDTO DeleteSectionByUUID(string uuid);
    }
}
