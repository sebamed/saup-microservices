using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingMicroservice.DTO.User;
using MessagingMicroservice.DTO.User.Request;

namespace MessagingMicroservice.Services {
    public interface IUserService : ICrudService<UserResponseDTO> {

        UserResponseDTO Create(CreateUserRequestDTO requestDTO);

        UserResponseDTO Update(UpdateUserRequestDTO requestDTO);

    }
}
