using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Domain;
using UserMicroservice.DTO.User;
using UserMicroservice.DTO.User.Response;

namespace UserMicroservice.Mappers {
    public class MappingProfile : Profile {

        public MappingProfile() {
            // add all auto mappings here
            CreateMap<User, UserResponseDTO>();
            CreateMap<Role, RoleResponseDTO>();
        }

    }
}
