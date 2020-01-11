using LectureMaterialMicroservice.Domain;
using SectionMicroservice.Domain.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Domain
{
    public class Material {
        public Section section { get; set; }

        public File file { get; set; }

        public int visible { get; set; }
    }
}
