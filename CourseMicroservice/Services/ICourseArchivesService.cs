using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseArchivesService {
        //GET METHODS
        List<CourseArchiveResponseDTO> GetAllCourseArchives(string uuid);
        CourseArchiveResponseDTO CreateCourseArchive(CreateCourseArchiveRequestDTO request);
    }
}
