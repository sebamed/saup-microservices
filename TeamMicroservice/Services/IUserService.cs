using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.DTO.User;
using TeamMicroservice.DTO.User.Request;

namespace TeamMicroservice.Services {
    public interface IUserService : ICrudService<UserResponseDTO> {

        UserResponseDTO Create(CreateUserRequestDTO requestDTO);

        UserResponseDTO Update(UpdateUserRequestDTO requestDTO);

    }
}
