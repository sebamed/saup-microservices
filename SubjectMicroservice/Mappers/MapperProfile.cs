using AutoMapper;
using SubjectMicroservice.Domain;
using SubjectMicroservice.DTO.Subject;
using SubjectMicroservice.DTO.Subject.Request;
using SubjectMicroservice.DTO.Subject.Response;
using SubjectMicroservice.DTO.SubjectArchive;
using SubjectMicroservice.DTO.SubjectArchive.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.Mappers
{
	public class MappingProfile : Profile{
        public MappingProfile(){

            CreateMap<Subject, SubjectResponseDTO>();

            CreateMap<SubjectArchive, SubjectArchiveResponseDTO>();
        }
    }
}


