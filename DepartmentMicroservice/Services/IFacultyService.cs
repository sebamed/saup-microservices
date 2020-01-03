using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.DTO.User.Request;

namespace DepartmentMicroservice.Services {
    public interface IFacultyService : ICrudService<FacultyResponseDTO> {
        FacultyResponseDTO Create(CreateFacultyRequestDTO requestDTO);
        FacultyResponseDTO Update(UpdateFacultyRequestDTO requestDTO);
        FacultyResponseDTO Delete(string uuid);

        List<FacultyResponseDTO> GetByName(string name);
        List<FacultyResponseDTO> GetByCity(string city);
    }
}
