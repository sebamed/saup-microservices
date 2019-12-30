using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileMicroservice.DTO.User;
using FileMicroservice.DTO.User.Request;

namespace FileMicroservice.Services {
    public interface IUserService : ICrudService<UserResponseDTO> {

        UserResponseDTO Create(CreateUserRequestDTO requestDTO);

        UserResponseDTO Update(UpdateUserRequestDTO requestDTO);

    }
}
