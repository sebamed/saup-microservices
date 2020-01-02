using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.DTO.Admin.Request;
using UserMicroservice.DTO.Admin.Response;

namespace UserMicroservice.Services {
    public interface IAdminService : ICrudService<AdminResponseDTO> {
        AdminResponseDTO Create(CreateAdminRequestDTO requestDTO);
    }
}
