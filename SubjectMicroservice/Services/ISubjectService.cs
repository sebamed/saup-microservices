using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectMicroservice.DTO.Subject;
using SubjectMicroservice.DTO.Subject.Request;
using SubjectMicroservice.DTO.Subject.Response;

namespace SubjectMicroservice.Services {
    public interface ISubjectService : ICrudService<SubjectResponseDTO> {

        SubjectResponseDTO Create(CreateSubjectRequestDTO requestDTO);

        SubjectResponseDTO Update(UpdateSubjectRequestDTO requestDTO);

        SubjectResponseDTO Delete(string uuid);

        List<SubjectResponseDTO> GetByName(string name);

    }
}
