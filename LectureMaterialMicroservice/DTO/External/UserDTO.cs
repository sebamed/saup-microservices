using Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.External
{
    public class UserDTO : BaseDTO {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
    }
}
