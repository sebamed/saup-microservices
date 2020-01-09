using Commons.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.DTO.User.Request {
    public class UpdateFacultyRequestDTO: BaseDTO {
        [Required]
        public string name { get; set; }

        [Required]
        public string city { get; set; }

        [Phone]
        public string phone { get; set; }
    }
}
