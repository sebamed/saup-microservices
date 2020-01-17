using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Domain;
using UserMicroservice.DTO.Admin.Response;
using UserMicroservice.DTO.Student.Response;
using UserMicroservice.DTO.Teacher.Response;
using UserMicroservice.DTO.User;
using UserMicroservice.DTO.User.Request;
using UserMicroservice.DTO.User.Response;

namespace UserMicroservice.Mappers {
    public class MappingProfile : Profile {

        public MappingProfile() {
            // add all auto mappings here
            CreateMap<User, UserResponseDTO>();
            CreateMap<User, AdminResponseDTO>();
            CreateMap<User, StudentResponseDTO>();
            CreateMap<Role, RoleResponseDTO>();
            CreateMap<Admin, AdminResponseDTO>();
            CreateMap<UpdateUserRequestDTO, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Student, StudentResponseDTO>();
            CreateMap<Teacher, TeacherResponseDTO>();
            CreateMap<Student, StudentWithDepartmantResponseDTO>();
        }

    }
}
