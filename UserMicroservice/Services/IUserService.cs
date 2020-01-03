using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Domain;
using UserMicroservice.DTO.User;
using UserMicroservice.DTO.User.Request;

namespace UserMicroservice.Services {
    public interface IUserService : ICrudService<UserResponseDTO> {

        User Create(CreateUserRequestDTO requestDTO);

        UserResponseDTO Update(UpdateUserRequestDTO requestDTO);

        User FindOneByUuidOrThrow(string uuid);

        User FindOneByEmailAddress(string email);
    }
}
