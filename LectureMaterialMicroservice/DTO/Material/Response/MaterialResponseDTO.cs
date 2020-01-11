using Commons.DTO;
using LectureMaterialMicroservice.DTO.User;
using SectionMicroservice.Domain.External;
using SectionMicroservice.DTO.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.Material.Response
{
    public class MaterialResponseDTO {
        public SectionResponseDTO section { get; set; }

        public FileDTO file { get; set; }

        public int visible { get; set; }
    }
}
