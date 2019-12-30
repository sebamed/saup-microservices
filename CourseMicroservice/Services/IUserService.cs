using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseMicroservice.DTO.User;
using CourseMicroservice.DTO.User.Request;

namespace CourseMicroservice.Services {
    public interface IUserService : ICrudService<UserResponseDTO> {

        UserResponseDTO Create(CreateUserRequestDTO requestDTO);

        UserResponseDTO Update(UpdateUserRequestDTO requestDTO);

    }
}
