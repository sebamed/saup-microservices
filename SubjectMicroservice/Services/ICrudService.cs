﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.Services {
    public interface ICrudService<T> {

        List<T> GetAll();
    }
}
