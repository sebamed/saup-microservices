using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseService {

        //GET METHODS
        List<CourseMultipleResponseDTO> GetAll();
        CourseResponseDTO GetOneByUuid(string uuid);

        //POST METHODS
        CourseResponseDTO Create(CreateCourseRequestDTO requestDTO);

        //PUT METHODS
        CourseResponseDTO Update(UpdateCourseRequestDTO requestDTO);

        //DELETE METHODS
        CourseResponseDTO Delete(string uuid);

    }
}
