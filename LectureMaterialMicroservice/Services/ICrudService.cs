﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Services {
    public interface ICrudService<T> {

        List<T> GetAll();
        List<T> GetVisibleSections();

        T GetOneByUuid(string uuid);

    }
}
