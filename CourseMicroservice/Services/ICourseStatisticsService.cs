using Commons.Domain;
using CourseMicroservice.DTO.Course;
using System.Collections.Generic;

namespace CourseMicroservice.Services {
    public interface ICourseStatisticsService {
        //GET METHODS
        CourseStatisticsResponseDTO Get_Course_Statistics_Course_Uuid(string courseUuid);
        CourseStatisticsResponseDTO Get_Course_Satistics_Student_Uuid(string studentUuid);
        CourseStatisticsResponseDTO Get_Course_Statistics_Year(int year);
    }
}
