using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.DTO.User.Request;

namespace DepartmentMicroservice.Services {
    public interface IDepartmentService : ICrudService<DepartmentResponseDTO> {
		DepartmentResponseDTO Create(CreateDepartmentRequestDTO requestDTO);
		DepartmentResponseDTO Update(UpdateDepartmentRequestDTO requestDTO);
		DepartmentResponseDTO Delete(string uuid);

        List<DepartmentResponseDTO> GetByName(string name);
        List<DepartmentResponseDTO> GetByCity(string city);
    }
}
