using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectMicroservice.DTO.SubjectArchive;
using SubjectMicroservice.DTO.SubjectArchive.Request;
using SubjectMicroservice.DTO.SubjectArchive.Response;

namespace SubjectMicroservice.Services
{
	public interface ISubjectArchiveService : ICrudService<SubjectArchiveResponseDTO>
	{
        SubjectArchiveResponseDTO Create(CreateSubjectArchiveRequestDTO requestDTO);

        SubjectArchiveResponseDTO Update(UpdateSubjectArchiveRequestDTO requestDTO);

        SubjectArchiveResponseDTO Delete(string uuid);

        List<SubjectArchiveResponseDTO> GetByName(string name);

    }
}
