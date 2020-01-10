using AutoMapper;
using LectureMaterialMicroservice.Domain;
using LectureMaterialMicroservice.DTO.User;
using SectionMicroservice.Domain;
using SectionMicroservice.Domain.External;
using SectionMicroservice.DTO.External;
using SectionMicroservice.DTO.SectionArchive.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Mappers
{
    public class MappingProfile : Profile {

        public MappingProfile() {
            //and all auto mappings here
            CreateMap<Section, SectionResponseDTO>();
            CreateMap<Section, MultipleSectionResponseDTO>();
            CreateMap<SectionArchive, SectionArchiveResponseDTO>();
            CreateMap<SectionArchive, MultipleSectionArchiveResponseDTO>();
            CreateMap<Course, CourseDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
