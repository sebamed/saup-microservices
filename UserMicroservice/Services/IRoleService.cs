using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Domain;

namespace UserMicroservice.Services {
    public interface IRoleService {

        Role FindOneByName(string name);

    }
}
