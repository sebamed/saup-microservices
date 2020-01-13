using Commons.Consts;
using Commons.DatabaseUtils;
using Commons.ExceptionHandling.Exceptions;
using MessagingMicroservice.Consts;
using MessagingMicroservice.DTO.Message;
using MessagingMicroservice.Mappers;
using MessagingtMicroservice.Services;
using System.Collections.Generic;
using System;
using MessagingMicroservice.Domain;
using Microsoft.AspNetCore.Http;
using Commons.Domain;
using AutoMapper;
using Commons.HttpClientRequests;
using MessagingMicroservice.DTO;
using System.Net.Http;

namespace MessagingMicroservice.Services.Implementation
{
    public class MessagingService : IMessagingService {
        private readonly QueryExecutor _queryExecutor;
        private readonly ModelMapper _modelMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _autoMapper;
        private readonly SqlCommands _sqlCommands;
        private readonly HttpClientService _httpClientService;
        public MessagingService(
            QueryExecutor queryExecutor, 
            ModelMapper modelMapper, 
            IHttpContextAccessor httpContextAccessor, 
            IMapper autoMapper,
            SqlCommands sqlCommands,
            HttpClientService httpClientService
            ) 
        {
            this._queryExecutor = queryExecutor;
            this._modelMapper = modelMapper;
            this._httpContextAccessor = httpContextAccessor;
            this._autoMapper = autoMapper;
            this._sqlCommands = sqlCommands;
            this._httpClientService = httpClientService;
        }

        public MessageResponseDTO Create(CreateMessageRequestDTO requestDTO)
        {    
            //CREATE MESSAGE
            Message message = new Message()
            {
                content = requestDTO.content,
                dateTime = DateTime.Now
            };

            var userPrincipal = new UserPrincipal(_httpContextAccessor.HttpContext);
            message.sender = this._httpClientService.SendRequest<User>(HttpMethod.Get, "http://localhost:40001/api/users/" + userPrincipal.uuid, userPrincipal.token).Result;
            message = this._queryExecutor.Execute<Message>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_MESSAGE(message), this._modelMapper.MapToMessage);

            //ADD FILES TO MESSAGE
            List<File> addedFiles = new List<File>(requestDTO.files.Count);
            if(requestDTO.files != null)
            {
                foreach (var f in requestDTO.files)
                {
                    var file = new File() { uuid = f.uuid, filePath = f.filePath };
                    FileMessage fm = new FileMessage()
                    {
                        messageUUID = message.uuid,
                        file = file
                    };

                    fm = this._queryExecutor.Execute<FileMessage>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_FILE_MESSAGE(fm), this._modelMapper.MapToFileMessage);
                    addedFiles.Add(file);
                }
            }

            //SEND MESSAGE TO RECIPIENTS
            List<User> recipients = new List<User>();
            foreach (var rDTO in requestDTO.recipients)
            {
                var userRecipient = new User()
                {
                    uuid = rDTO.uuid,
                    name = rDTO.name,
                    surname = rDTO.surname
                };

                Recipient messageRecipient = new Recipient()
                {
                    messageUUID = message.uuid,
                    recipient = userRecipient
                };
                this._queryExecutor.Execute<Recipient>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_RECIPIENT(messageRecipient), this._modelMapper.MapToRecipient);
                recipients.Add(userRecipient);
            }

            message.recipients = recipients;
            message.files = addedFiles;
            return this._autoMapper.Map<MessageResponseDTO>(message);
        }

        public MessageResponseDTO Update(UpdateMessageRequestDTO requestDTO)
        {
            if (this.FindOneByUUID(requestDTO.uuid) == null)
                throw new EntityNotFoundException($"Message with uuid {requestDTO.uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            Message message = new Message()
            {
                uuid = requestDTO.uuid,
                content = requestDTO.content,
                dateTime = requestDTO.dateTime
            };

            var userPrincipal = new UserPrincipal(_httpContextAccessor.HttpContext);
            message.sender = this._httpClientService.SendRequest<User>(HttpMethod.Get, "http://localhost:40001/api/users/" + userPrincipal.uuid, userPrincipal.token).Result;


            //todo pozivanje update fajla
            //todo definisati da se reciepenti ne mogu menjati/dodavati za istu poruku?

            //todo da li treba MapToMessageAfterInserted?
            message = this._queryExecutor.Execute<Message>(DatabaseConsts.USER_SCHEMA, _sqlCommands.UPDATE_MESSAGE(message), _modelMapper.MapToMessage);

            return this._autoMapper.Map<MessageResponseDTO>(message);
        }

        public MessageResponseDTO GetOneByUuid(string uuid)
        {
            return this._autoMapper.Map<MessageResponseDTO>(this.FindOneByUUID(uuid));
        }

        private Message FindOneByUUID(string uuid)
        {
            //todo pronaci u FileMessage fajlove i u recipient primaoce
            return this._queryExecutor.Execute<Message>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_MESSAGE_BY_UUID(uuid), this._modelMapper.MapToMessage);
        }
        public List<MessageResponseDTO> GetAll()
        {
            return this._autoMapper.Map<List<MessageResponseDTO>>(this.FindAll());
        }

        private List<Message> FindAll()
        {
            //todo pronaci u FileMessage fajlove i u recipient primaoce
            return this._queryExecutor.Execute<List<Message>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_ALL_MESSAGES(), this._modelMapper.MapToMessages);
        }

        public MessageResponseDTO Delete(string uuid)
        {
            var toDelete = FindOneByUUID(uuid);
            if (toDelete == null)
                throw new EntityNotFoundException($"Message with uuid {uuid} doesn't exist!", GeneralConsts.MICROSERVICE_NAME);

            this._queryExecutor.Execute(DatabaseConsts.USER_SCHEMA, _sqlCommands.DELETE_RECIPIENT_BY_MESSAGE(uuid), this._modelMapper.MapToMessage);
            this._queryExecutor.Execute(DatabaseConsts.USER_SCHEMA, _sqlCommands.DELETE_FILE_MESSAGE_BY_MESSAGE(uuid), this._modelMapper.MapToMessage);
            this._queryExecutor.Execute(DatabaseConsts.USER_SCHEMA, _sqlCommands.DELETE_MESSAGE(uuid), this._modelMapper.MapToMessage);

            return this._autoMapper.Map<MessageResponseDTO>(toDelete);
        }   

    }
}
