using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileMicroservice.DTO;
using FileMicroservice.DTO.File;
using FileMicroservice.DTO.File.Request;

namespace FileMicroservice.Services {
    public interface IFileService : ICrudService<FileResponseDTO> {
        FileResponseDTO Create(CreateFileRequestDTO requestDTO);
        FileResponseDTO Update(UpdateFileRequestDTO requestDTO);
        FileResponseDTO Delete(string uuid);
        Stream GetFileByPath(string path);
    }
}
