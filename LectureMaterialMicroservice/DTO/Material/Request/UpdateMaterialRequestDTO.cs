﻿using Commons.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.DTO.Material.Request
{
    public class UpdateMaterialRequestDTO : BaseDTO {

        [Required]
        public string sectionUUID { get; set; }

        [Required]
        public string fileUUID { get; set; }

        [Required]
        public string filePath { get; set; }

        [Required]
        public int visible { get; set; }
    }
}
