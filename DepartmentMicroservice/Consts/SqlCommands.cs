using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentMicroservice.Consts {
    public class SqlCommands {
        public string GET_ALL_FACULTIES() {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Faculty";
        }
    }
}
