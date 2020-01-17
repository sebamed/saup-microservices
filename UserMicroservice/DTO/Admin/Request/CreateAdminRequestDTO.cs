using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.DTO.Admin.Request {
    public class CreateAdminRequestDTO {

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
