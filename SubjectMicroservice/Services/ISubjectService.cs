using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SubjectMicroservice.DTO.Subject;
using SubjectMicroservice.DTO.Subject.Request;
using SubjectMicroservice.DTO.Subject.Response;

namespace SubjectMicroservice.Services {
    public interface ISubjectService : ICrudService<MultipleSubjectResponseDTO> {

        SubjectResponseDTO Create(CreateSubjectRequestDTO requestDTO);

        SubjectResponseDTO Update(UpdateSubjectRequestDTO requestDTO);

        SubjectResponseDTO Delete(string uuid);


        SubjectResponseDTO GetOneByUuid(string uuid);

        List<MultipleSubjectResponseDTO> GetByName(string name);

        List<MultipleSubjectResponseDTO> GetByDepartmentUUID(string uuid);
        List<MultipleSubjectResponseDTO> GetByCreatorUUID(string uuid);
    }
}
