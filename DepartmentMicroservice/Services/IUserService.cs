using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DepartmentMicroservice.DTO.User;
using DepartmentMicroservice.DTO.User.Request;

namespace DepartmentMicroservice.Services {
    public interface IUserService : ICrudService<UserResponseDTO> {

        UserResponseDTO Create(CreateUserRequestDTO requestDTO);

        UserResponseDTO Update(UpdateUserRequestDTO requestDTO);

    }
}
