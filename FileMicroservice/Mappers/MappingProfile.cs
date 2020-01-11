using AutoMapper;
using FileMicroservice.Domain;
using FileMicroservice.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Mappers {
    public class MappingProfile: Profile {

        public MappingProfile() {
            //TODO: Define how to map different object from DB to ResponseDTO 
            CreateMap<File, FileResponseDTO>();
        }
    }
}
