using AutoMapper;
using SubjectMicroservice.Domain;
using SubjectMicroservice.Domain.External;
using SubjectMicroservice.DTO.Department;
using SubjectMicroservice.DTO.External;
using SubjectMicroservice.DTO.Subject.Response;
using SubjectMicroservice.DTO.SubjectArchive.Response;

namespace SubjectMicroservice.Mappers
{
	public class MappingProfile : Profile{
        public MappingProfile(){
            CreateMap<Subject, SubjectResponseDTO>();
            CreateMap<Subject, MultipleSubjectResponseDTO>();
            CreateMap<SubjectArchive, SubjectArchiveResponseDTO>();
            CreateMap<Faculty, FacultyDTO>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}


