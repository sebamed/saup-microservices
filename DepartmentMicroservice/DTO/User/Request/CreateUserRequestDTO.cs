﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.DTO.User {
    public class CreateUserRequestDTO {

        public string username { get; set; }

        public int age { get; set; }

    }
}