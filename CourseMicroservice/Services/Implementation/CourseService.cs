using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using CourseMicroservice.Consts;
using CourseMicroservice.Domain;
using CourseMicroservice.Mappers;
using CourseMicroservice.DTO.Course;
using AutoMapper;

namespace CourseMicroservice.Services.Implementation {
    public class CourseService : ICourseService
    {
        private readonly IMapper _autoMapper;
        private readonly QueryExecutor _queryExecutor;
        private readonly SqlCommands _sqlCommands;
        private readonly ModelMapper _modelMapper;

        public CourseService(QueryExecutor queryExecutor, IMapper autoMapper, ModelMapper modelMapper, SqlCommands sqlCommands)
        {
            this._autoMapper = autoMapper;
            this._queryExecutor = queryExecutor;
            this._sqlCommands = sqlCommands;
            this._modelMapper = modelMapper;
        }

        //izvrsava query nad bazom, vraca domain klase
        //3 parametara: sema, sql komanda i model mapper
        //model mapper mapira iz kverija u domain klasu
        public List<Course> FindAll()
        {
            return this._queryExecutor.Execute<List<Course>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_COURSES(), this._modelMapper.MapToCourses);
        }

        public Course FindOneByUuidOrThrow(string uuid)
        {
            Course course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ONE_COURSE_BY_UUID(uuid), this._modelMapper.MapToCourse);
            if (course == null)
            {
                throw new EntityNotFoundException($"Course with uuid: {uuid} does not exist!", GeneralConsts.MICROSERVICE_NAME);
            }
            return course;
        }

        //mapira rezultate funkcije FindALL u DTO klasu
        //koristi automapper
        public List<CourseResponseDTO> GetAll()
        {
            return this._autoMapper.Map<List<CourseResponseDTO>>((this.FindAll()));
        }


        public CourseResponseDTO GetOneByUuid(string uuid)
        {
            return this._autoMapper.Map<CourseResponseDTO>(this.FindOneByUuidOrThrow(uuid));
        }

        CourseResponseDTO ICourseService.Create(CreateCourseRequestDTO requestDTO)
        {
            Course course = new Course()
            {
                name = requestDTO.name,
                description = requestDTO.description,
                active = requestDTO.active,
                minStudents = requestDTO.minStudents,
                maxStudents = requestDTO.maxtudents,
                creationDate = requestDTO.creationDate,
                subjectUUID = requestDTO.subjectUUID
            };
            this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_COURSE(course), this._modelMapper.MapToCourseAfterInsert);
            return this._autoMapper.Map<CourseResponseDTO>(course);
        }

        public CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO)
        {
            Course course = this.FindOneByUuidOrThrow(requestDTO.uuid);
            course = this._autoMapper.Map<Course>(requestDTO);

            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_COURSE(course), this._modelMapper.MapToCourseAfterUpdate);

            return this._autoMapper.Map<CourseResponseDTO>(course);
        }

        public CourseResponseDTO Delete(string uuid)
        {
            Course course = this.FindOneByUuidOrThrow(uuid);
            course = this._queryExecutor.Execute<Course>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_COURSE(uuid), this._modelMapper.MapToCourse);
            return this._autoMapper.Map<CourseResponseDTO>(course);
        }
    }
}
