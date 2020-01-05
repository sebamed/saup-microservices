using AutoMapper;
using DepartmentMicroservice.Domain;
using DepartmentMicroservice.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Mappers {
    public class MappingProfile: Profile {

        public MappingProfile() {

            CreateMap<Faculty, FacultyResponseDTO>();
            CreateMap<FacultyResponseDTO, Faculty>();
            CreateMap<Department, DepartmentResponseDTO>();
        }
    }
}
