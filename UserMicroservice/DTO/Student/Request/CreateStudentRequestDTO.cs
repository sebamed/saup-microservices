using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.DTO.Student.Request {
    public class CreateStudentRequestDTO {

        [Required]
        public string name { get; set; }

        [Required]
        public string surname { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Phone]
        public string phone { get; set; }

        [Required]
        public string departmentUuid { get; set; }

        [Required]
        public string indexNumber { get; set; }


    }
}
