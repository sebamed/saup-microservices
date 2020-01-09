using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectMicroservice.DTO.SubjectArchive;
using SubjectMicroservice.DTO.SubjectArchive.Request;
using SubjectMicroservice.DTO.SubjectArchive.Response;

namespace SubjectMicroservice.Services
{
	public interface ISubjectArchiveService {
        SubjectArchiveResponseDTO Create(CreateSubjectArchiveRequestDTO requestDTO);

        SubjectArchiveResponseDTO GetLatestVersionBySubjectUUID(string sectionUUID);

        List<MultipleSubjectArchiveResponseDTO> GetAllArchivesBySubjectUUID(string sectionUUID);

        public void Delete(string subjectUUID);
    }
}
