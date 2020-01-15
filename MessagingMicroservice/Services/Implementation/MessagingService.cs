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
using Commons.DTO;

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
            UserPrincipal userPrincipal;
            try
            {
                userPrincipal = new UserPrincipal(_httpContextAccessor.HttpContext);
                message.sender = this._httpClientService.SendRequest<User>(HttpMethod.Get, "http://localhost:40001/api/users/" + userPrincipal.uuid, userPrincipal.token).Result;
                message = this._queryExecutor.Execute<Message>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_MESSAGE(message), this._modelMapper.MapToMessage);
            } catch
            {
                throw new BaseException("Error while getting sender, probably User microservice is down", GeneralConsts.MICROSERVICE_NAME, System.Net.HttpStatusCode.ServiceUnavailable);
            }
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
            if (requestDTO is CreateUserMessageRequestDTO){
                var recipientUUID = ((CreateUserMessageRequestDTO) requestDTO).recipientUUID;
                recipients.Add(SendMessageToRecipient(recipientUUID, message, recipientMails, userPrincipal.token));
            } else{
                try
                {
                    var team = ((CreateTeamMessageRequestDTO) requestDTO).teamUUID;
                    var studentsInTeam = this._httpClientService.SendRequest<List<BaseDTO>>(HttpMethod.Get, "http://localhost:40004/api/teams/students/" + team, userPrincipal.token).Result;
                    if(studentsInTeam == null)
                    {
                        throw new EntityNotFoundException($"There is no students in team {team}", GeneralConsts.MICROSERVICE_NAME);
                    }

                    foreach (var student in studentsInTeam)
                    {
                        recipients.Add(SendMessageToRecipient(student.uuid, message, recipientMails, userPrincipal.token));
                    }
                }catch(Exception e) {
                    throw new BaseException("Team service is not available", GeneralConsts.MICROSERVICE_NAME, System.Net.HttpStatusCode.ServiceUnavailable);
                }
            }

            this._httpClientService.SendEmail(recipientMails, $"Nova poruka: {message.sender.name}", message.content);
            message.recipients = recipients;
            message.files = addedFiles;
            return this._autoMapper.Map<MessageResponseDTO>(message);
        }

        private User SendMessageToRecipient(string uuid, Message message, List<string> recipientMails, string token)
        {
            var r = this._httpClientService.SendRequest<User>(HttpMethod.Get, "http://localhost:40001/api/users/" + uuid, token).Result;
            if (r == null)
                throw new EntityNotFoundException($"There is no recipient with uuid {uuid}", GeneralConsts.SCHEMA_NAME);
            var userRecipient = new User()
            {
                uuid = r.uuid,
                name = r.name,
                surname = r.surname
            };

            Recipient messageRecipient = new Recipient()
            {
                messageUUID = message.uuid,
                recipient = userRecipient
            };
            this._queryExecutor.Execute<Recipient>(DatabaseConsts.USER_SCHEMA, this._sqlCommands.CREATE_RECIPIENT(messageRecipient), this._modelMapper.MapToRecipient);

            recipientMails.Add(r.email);
            return userRecipient;
        }

        private List<Message> FindMessagesByRecipients(string[] recipients)
        {
            string sender = new UserPrincipal(_httpContextAccessor.HttpContext).uuid;
            List<Message> messages = this._queryExecutor.Execute<List<Message>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_MESSAGES_BY_RECIPIENT(recipients, sender), this._modelMapper.MapToMessages);
            if (messages == null)
                throw new EntityNotFoundException($"There is no messages by recipient {recipients}", GeneralConsts.MICROSERVICE_NAME);

            foreach (var message in messages)
            {
                List<User> recipientsM = this._queryExecutor.Execute<List<User>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_RECIPIENTS_BY_MESSAGE(message.uuid), this._modelMapper.MapToUsers);
                List<File> files = this._queryExecutor.Execute<List<File>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.GET_FILES_BY_MESSAGE(message.uuid), this._modelMapper.MapToFiles);
                message.recipients = recipientsM;
                message.files = files;
            }
            return messages;
        }
        public List<MessageResponseDTO> GetMessagesByRecipents(string recipentUUIDs)
        {
            string[] split = recipentUUIDs.Split(',');
            return this._autoMapper.Map<List<MessageResponseDTO>>(FindMessagesByRecipients(split));
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

            if (messages == null) messages = new List<Message>();

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

        public string UpdateFileInMessage(FileDTO fileDTO)
        {
            File file = new File()
            {
                uuid = fileDTO.uuid,
                filePath = fileDTO.filePath
            };

            List<File> r = this._queryExecutor.Execute<List<File>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.UPDATE_FILE_IN_MESSAGE(file), this._modelMapper.MapToFiles);
            if (r == null)
            {
                return $"There is no file with uuid {file.uuid}";
            }
            return "File updated";
        }

        public string UpdateRecipientInMessage(UserDTO userDTO)
        {
            User user = new User()
            {
                uuid = userDTO.uuid,
                name = userDTO.name,
                surname = userDTO.surname
            };

            List<Recipient> r = this._queryExecutor.Execute<List<Recipient>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.UPDATE_RECIPIENT_IN_MESSAGE(user), this._modelMapper.MapToRecipients);
            if (r == null)
            {
                return $"There is no recipient with uuid {user.uuid}";
            }
            return "Recipient updated";
        }

        public string UpdateSenderInMessage(UserDTO userDTO)
        {
            User user = new User()
            {
                uuid = userDTO.uuid,
                name = userDTO.name,
                surname = userDTO.surname
            };

            List<Message> r = this._queryExecutor.Execute<List<Message>>(DatabaseConsts.USER_SCHEMA, _sqlCommands.UPDATE_SENDER_IN_MESSAGE(user), this._modelMapper.MapToMessages);

            if(r == null)
            {
                return $"There is no sender with uuid {user.uuid}";
            }
            return "Sender updated";
        }

        public List<MessageResponseDTO> GetMessagesByTeam(string team)
        {
            try
            {
                var studentsInTeam = this._httpClientService.SendRequest<List<BaseDTO>>(HttpMethod.Get, "http://localhost:40004/api/teams/students/" + team, new UserPrincipal(_httpContextAccessor.HttpContext).token).Result;
                if (studentsInTeam == null)
                {
                    throw new EntityNotFoundException($"There is no students in team {team}", GeneralConsts.MICROSERVICE_NAME);
                }
                string[] array = new string[studentsInTeam.Count];
                for (int i = 0; i < studentsInTeam.Count; i++){
                    array[i] = studentsInTeam[i].uuid;
                }
                
                return this._autoMapper.Map<List<MessageResponseDTO>>(FindMessagesByRecipients(array));
            }
            catch (Exception e)
            {
                throw new BaseException("Team service is not available", GeneralConsts.MICROSERVICE_NAME, System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }
    }
}
