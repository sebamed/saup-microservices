using AutoMapper;
using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.Domain;
using TeamMicroservice.Domain.External;
using TeamMicroservice.DTO;
using TeamMicroservice.DTO.External;
using TeamMicroservice.DTO.Team.Response;

namespace TeamMicroservice.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamResponseDTO>();
            CreateMap<Team, MultipleTeamResponseDTO>();
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<Course, CourseDTO>();
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentTeam, StudentTeamResponseDTO>();
            CreateMap<StudentTeamResponseDTO, StudentTeam>();
            CreateMap<TeamResponseDTO, Team>();
            CreateMap<Student, BaseDTO>();
        }
    }
}
