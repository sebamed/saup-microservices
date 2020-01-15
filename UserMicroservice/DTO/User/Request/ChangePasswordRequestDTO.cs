using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.DTO.User.Request {
    public class ChangePasswordRequestDTO {

        [Required]
        public string oldPassword { get; set; }

        [Required]
        public string newPassword { get; set; }

        [Required]
        public string confirmNewPassword { get; set; }

    }
}
