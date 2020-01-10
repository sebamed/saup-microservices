using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using AutoMapper;
using FileMicroservice.DTO;
using FileMicroservice.DTO.File;
using FileMicroservice.Mappers;
using FileMicroservice.Consts;
using FileMicroservice.Domain;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using FileMicroservice.DTO.File.Request;

namespace FileMicroservice.Services.Implementation {
    public class FileService : IFileService {

        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly IMapper _autoMapper;
        private readonly IWebHostEnvironment _env;
        public FileService(QueryExecutor queryExecutor, ModelMapper modelMapper, SqlCommands sqlCommands, IMapper autoMapper, IWebHostEnvironment env) {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._sqlCommands = sqlCommands;
            this._autoMapper = autoMapper;
            this._env = env;
        }

        public FileResponseDTO Create(CreateFileRequestDTO requestDTO)
        {
            Domain.File file = new Domain.File();
            file.filePath = CreateRelativeFilePath(file.uuid, requestDTO.fileExtension);
            SaveFile(requestDTO.fileData, file.filePath);

            file = this._queryExecutor.Execute<Domain.File>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_FILE(file), this._modelMapper.MapToFileAfterInsert);
            file = this._queryExecutor.Execute<Domain.File>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_FILE_BY_UUID(file.uuid), this._modelMapper.MapToFile);
            return this._autoMapper.Map<FileResponseDTO>(file);
        }

        public FileResponseDTO Delete(string uuid)
        {
            Domain.File toDelete = this.FindOneByUUID(uuid);
            if (toDelete == null)
                throw new EntityNotFoundException($"File with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);
            this._queryExecutor.Execute(DatabaseConsts.USER_SCHEMA, this._sqlCommands.DELETE_FILE(uuid), this._modelMapper.MapToFile);
            DeleteFile(toDelete.filePath);
            return this._autoMapper.Map<FileResponseDTO>(toDelete);
        }

        public List<Domain.File> FindAll()
        {
            return this._queryExecutor.Execute<List<Domain.File>>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.GET_ALL_FILES(), this._modelMapper.MapToFiles);
        }

        public List<FileResponseDTO> GetAll()
        {
            return this._autoMapper.Map<List<FileResponseDTO>>(this.FindAll());
        }

        private Domain.File FindOneByUUID(string uuid)
        {
            return this._queryExecutor.Execute<Domain.File>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_FILE_BY_UUID(uuid), this._modelMapper.MapToFile);
        }

        public FileResponseDTO GetOneByUuid(string uuid)
        {
            return this._autoMapper.Map<FileResponseDTO>(this.FindOneByUUID(uuid));
        }

        public FileResponseDTO Update(UpdateFileRequestDTO requestDTO)
        {
            if(this.FindOneByUUID(requestDTO.uuid) == null) 
                throw new EntityNotFoundException($"File with uuid {requestDTO.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            Domain.File file = new Domain.File()
            {
                uuid = requestDTO.uuid
            };

            file.filePath = CreateRelativeFilePath(file.uuid, requestDTO.fileExtension);
            DeleteFile(file.filePath);
            SaveFile(requestDTO.fileData, file.filePath);

            file = this._queryExecutor.Execute<Domain.File>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.UPDATE_FILE(file), this._modelMapper.MapToFile);

            return this._autoMapper.Map<FileResponseDTO>(file);
        }

        public Stream GetFileByPath(string relativePath)
        {
            if (!System.IO.File.Exists(CreateAbsolutePath(relativePath)))
                throw new EntityNotFoundException("File does not exist", GeneralConsts.MICROSERVICE_NAME);
            return System.IO.File.OpenRead(CreateAbsolutePath(relativePath));
        }

        //Methods for File System
        private string CreateRelativeFilePath(string uuid, string extension)
        {
            return Path.Combine(GeneralConsts.FILE_LOCATION, $"{uuid}.{extension}");
        }

        private string CreateAbsolutePath(string relativePath)
        {
            return Path.Combine(_env.ContentRootPath, relativePath);
        }

        private void SaveFile(string fileData, string relativePath)
        {
            var dataByteArray = Convert.FromBase64String(fileData);
            int fileSize = dataByteArray.Length;
            if (fileSize > 5000000)
                throw new PayloadTooLargeException($"File is too large({fileSize}B), max size is 5MB", GeneralConsts.MICROSERVICE_NAME);

            var dataStream = new MemoryStream(dataByteArray)
            {
                Position = 0
            };

            var absolutePath = CreateAbsolutePath(relativePath);
            if (System.IO.File.Exists(absolutePath))
                throw new EntityAlreadyExistsException("File already exists", GeneralConsts.MICROSERVICE_NAME);
            using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
            {
                dataStream.WriteTo(fs);
            }
        }

        private void DeleteFile(string relativePath)
        {
            if (System.IO.File.Exists(CreateAbsolutePath(relativePath)))
            {
                System.IO.File.Delete(CreateAbsolutePath(relativePath));
            }
        }
    }
}
