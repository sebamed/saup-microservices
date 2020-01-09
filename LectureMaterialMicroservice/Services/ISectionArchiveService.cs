using LectureMaterialMicroservice.Services;
using SectionMicroservice.Domain;
using SectionMicroservice.DTO.SectionArchive.Request;
using SectionMicroservice.DTO.SectionArchive.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Services
{
    public interface ISectionArchiveService {
        SectionArchiveResponseDTO Create(CreateSectionArchiveRequestDTO requestDTO);
        SectionArchive GetOneByArchiveBySectionUuid(string sectionUUID);

    }
}
