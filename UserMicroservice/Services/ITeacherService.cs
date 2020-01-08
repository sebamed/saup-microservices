using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.DTO.Teacher.Request;
using UserMicroservice.DTO.Teacher.Response;

namespace UserMicroservice.Services {
    public interface ITeacherService  : ICrudService<TeacherResponseDTO> {

        TeacherResponseDTO Create(CreateTeacherRequestDTO requestDTO);

    }
}
