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
            List<File> addedFiles = new List<File>();
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
            List<string> recipientMails = new List<string>();
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
                var r = this._httpClientService.SendRequest<User>(HttpMethod.Get, "http://localhost:40001/api/users/" + rDTO.uuid, userPrincipal.token).Result;
                recipientMails.Add(r.email);
                recipients.Add(userRecipient);
            }
            this._httpClientService.SendEmail(recipientMails, $"Nova poruka: {message.sender.name}", message.content);
            message.recipients = recipients;
            message.files = addedFiles;
            return this._autoMapper.Map<MessageResponseDTO>(message);
        }

        public MessageResponseDTO Update(UpdateMessageRequestDTO requestDTO)
        {
            throw new NotImplementedException();
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
            message = this._queryExecutor.Execute<Message>(DatabaseConsts.USER_SCHEMA, _sqlCommands.UPDATE_MESSAGE(message), _modelMapper.MapToMessage);

            return this._autoMapper.Map<MessageResponseDTO>(message);
        }

        public List<MessageResponseDTO> GetMessagesByRecipents(string recipentUUIDs)
        {
            string sender = new UserPrincipal(_httpContextAccessor.HttpContext).uuid;
            string[] split = recipentUUIDs.Split(',');
            List<Message> messages = this._queryExecutor.Execute<List<Message>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_MESSAGES_BY_RECIPIENT(split, sender), this._modelMapper.MapToMessages);
            if (messages == null)
                throw new EntityNotFoundException($"There is no messages by recipient {recipentUUIDs}", GeneralConsts.MICROSERVICE_NAME);

            foreach (var message in messages)
            {
                List<User> recipients = this._queryExecutor.Execute<List<User>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_RECIPIENTS_BY_MESSAGE(message.uuid), this._modelMapper.MapToUsers);
                List<File> files = this._queryExecutor.Execute<List<File>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_FILES_BY_MESSAGE(message.uuid), this._modelMapper.MapToFiles);
                message.recipients = recipients;
                message.files = files;
            }
            return this._autoMapper.Map<List<MessageResponseDTO>>(messages);
        }

        public MessageResponseDTO GetOneByUuid(string uuid)
        {
            return this._autoMapper.Map<MessageResponseDTO>(this.FindOneByUUID(uuid));
        }

        private Message FindOneByUUID(string uuid)
        {
            var message = this._queryExecutor.Execute<Message>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_MESSAGE_BY_UUID(uuid), this._modelMapper.MapToMessage);
            List<User> recipients = this._queryExecutor.Execute<List<User>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_RECIPIENTS_BY_MESSAGE(uuid), this._modelMapper.MapToUsers);
            List<File> files = this._queryExecutor.Execute<List<File>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_FILES_BY_MESSAGE(uuid), this._modelMapper.MapToFiles);
            message.recipients = recipients;
            message.files = files;
            return message;
        }
        public List<MessageResponseDTO> GetAll()
        {
            return this._autoMapper.Map<List<MessageResponseDTO>>(this.FindAll());
        }

        private List<Message> FindAll()
        {
            List<Message> messages = this._queryExecutor.Execute<List<Message>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_ALL_MESSAGES(), this._modelMapper.MapToMessages);
            foreach(var message in messages)
            {
                List<User> recipients = this._queryExecutor.Execute<List<User>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_RECIPIENTS_BY_MESSAGE(message.uuid), this._modelMapper.MapToUsers);
                List<File> files = this._queryExecutor.Execute<List<File>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_FILES_BY_MESSAGE(message.uuid), this._modelMapper.MapToFiles);
                message.recipients = recipients;
                message.files = files;
            }
            return messages;
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
