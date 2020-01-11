using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileMicroservice.Services {
    public interface ICrudService<T> {
        List<T> GetAll();
        T GetOneByUuid(string uuid);
    }
}
