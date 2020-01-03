using AutoMapper;
using LectureMaterialMicroservice.Domain;
using LectureMaterialMicroservice.DTO.User;
using SectionMicroservice.Domain;
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
            CreateMap<SectionArchive, SectionArchiveResponseDTO>();
        }
    }
}
