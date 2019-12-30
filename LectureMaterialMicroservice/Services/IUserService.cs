using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LectureMaterialMicroservice.DTO.User;
using LectureMaterialMicroservice.DTO.User.Request;

namespace LectureMaterialMicroservice.Services {
    public interface IUserService : ICrudService<UserResponseDTO> {

        UserResponseDTO Create(CreateUserRequestDTO requestDTO);

        UserResponseDTO Update(UpdateUserRequestDTO requestDTO);

    }
}
