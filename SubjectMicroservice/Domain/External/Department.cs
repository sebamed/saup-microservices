using Commons.Domain;
using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectMicroservice.DTO.Department
{
    public class Department : BaseEntity
    {
        public string name { get; set; }

        public Faculty faculty { get; set; }
    }
}
