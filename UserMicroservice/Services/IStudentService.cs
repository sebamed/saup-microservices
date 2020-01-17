using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.DTO.Student.Request;
using UserMicroservice.DTO.Student.Response;

namespace UserMicroservice.Services {
    public interface IStudentService : ICrudService<StudentResponseDTO> {

        StudentResponseDTO Create(CreateStudentRequestDTO requestDTO);
        StudentWithDepartmantResponseDTO GetStudentInfoFromDepartmentByUuid(string uuid);
    }
}
