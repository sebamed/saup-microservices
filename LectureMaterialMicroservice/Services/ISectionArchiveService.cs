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

        List<MultipleSectionArchiveResponseDTO> GetAllArchivesBySectionUUID(string sectionUUID);

        SectionArchiveResponseDTO GetLatestVersionBySectionoUUID(string sectionUUID);

        SectionArchiveResponseDTO Create(CreateSectionArchiveRequestDTO requestDTO);

        public void Delete(string sectionUUID);
    }
}
