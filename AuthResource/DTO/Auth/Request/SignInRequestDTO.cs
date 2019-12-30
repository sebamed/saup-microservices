using System.ComponentModel.DataAnnotations;

namespace AuthResource.DTO.User {
    public class SignInRequestDTO {

        [EmailAddress]
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

    }
}
