using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseStatisticsService {
        //GET METHODS
        CourseStatisticsResponseDTO Get_Course_Statistics(string courseUuid);
    }
}
