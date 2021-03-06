﻿using Commons.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.DTO.User {
    public class CreateDepartmentRequestDTO {
        [Required]
        public string name { get; set; }

        public string facultyUUID { get; set; }
    }
}
