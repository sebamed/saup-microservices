using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MessagingMicroservice.Domain;

namespace MessagingMicroservice.Mappers {
    public class ModelMapper {
        public List<Message> MapToMessages(IDataReader reader) {
            List<Message> messages = new List<Message>();

            while(reader.Read()) {
                messages.Add(new Message() {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    content = Convert.ToString(reader["content"]),
                    dateTime = Convert.ToDateTime(reader["dateTime"]),
                    sender = new User(){
                       uuid = Convert.ToString(reader["senderUUID"]),
                       name = Convert.ToString(reader["senderName"]),
                       surname = Convert.ToString(reader["senderSurname"])
                    }
                    
                });
            }

            return messages;
        }

        public Message MapToMessage(IDataReader reader) {
            while (reader.Read()) {
                return new Message()
                {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    content = Convert.ToString(reader["content"]),
                    dateTime = Convert.ToDateTime(reader["dateTime"]),
                    sender = new User() {
                       uuid = Convert.ToString(reader["senderUUID"]),
                       name = Convert.ToString(reader["senderName"]),
                       surname = Convert.ToString(reader["senderSurname"])
                    }
                };
            }

            return null;
        }

   
        public List<Recipient> MapToRecipients(IDataReader reader)
        {
            List<Recipient> recipients = new List<Recipient>();

            while (reader.Read())
            {
                recipients.Add(new Recipient()
                {
                    messageUUID = Convert.ToString(reader["messageUUID"]),
                    recipient = new User() {
                        uuid = Convert.ToString(reader["recipientUUID"]),
                        name = Convert.ToString(reader["recipientName"]),
                        surname = Convert.ToString(reader["recipientSurname"])
                    }
                });
            }

            return recipients;
        }

        public List<User> MapToUsers(IDataReader reader)
        {
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User()
                {
                    uuid = Convert.ToString(reader["recipientUUID"]),
                    name = Convert.ToString(reader["recipientName"]),
                    surname = Convert.ToString(reader["recipientSurname"])
                });
            }

            return users;
        }

        public Recipient MapToRecipient(IDataReader reader)
        {
            while (reader.Read())
            {
                return new Recipient()
                {
                    messageUUID = Convert.ToString(reader["messageUUID"]),
                    recipient =
                    new User() {
                        uuid = Convert.ToString(reader["recipientUUID"]),
                        name = Convert.ToString(reader["recipientName"]),
                        surname = Convert.ToString(reader["recipientSurname"])
                    }
                };
            }

            return null;
        }

        internal FileMessage MapToFileMessage(IDataReader reader)
        {
            while (reader.Read())
            {
                return new FileMessage()
                {
                    messageUUID = Convert.ToString(reader["messageUUID"]),
                    file = new File()
                    {
                        uuid = Convert.ToString(reader["fileUUID"]),
                        filePath = Convert.ToString(reader["filePath"])
                    }
                };
            }
            return null;
        }

        public List<File> MapToFiles(IDataReader reader)
        {
            List<File> files = new List<File>();
            while (reader.Read())
            {
                files.Add(new File()
                {
                    uuid = Convert.ToString(reader["fileUUID"]),
                    filePath = Convert.ToString(reader["filePath"])
                });
            }
            return files;
        }
    }
}
