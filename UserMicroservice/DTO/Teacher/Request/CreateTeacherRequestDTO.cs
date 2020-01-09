using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.DTO.Teacher.Request {
    public class CreateTeacherRequestDTO {

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

    }
}
