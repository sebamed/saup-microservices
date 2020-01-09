using System;
using SubjectMicroservice.DTO.Department;
using SubjectMicroservice.DTO.External;

namespace SubjectMicroservice.DTO.SubjectArchive.Response {
    public class SubjectArchiveResponseDTO {

        public string subjectUUID { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public DateTime creationDate { get; set; }

        public DepartmentDTO department { get; set; }

        public UserDTO creator { get; set; }

        public UserDTO moderator { get; set; }

        public DateTime changeDate { get; set; }

        public int version { get; set; }
    }
}
