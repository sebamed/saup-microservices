using SectionMicroservice.DTO.Material.Request;
using SectionMicroservice.DTO.Material.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SectionMicroservice.Services
{
    public interface IMaterialService {
        MaterialResponseDTO Create(CreateMaterialRequestDTO requestDTO);

        MaterialResponseDTO Delete(string sectionUUID, string fileUUID);
    }
}
