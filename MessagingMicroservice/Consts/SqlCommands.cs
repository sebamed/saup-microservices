using MessagingMicroservice.Domain;
using System;

namespace MessagingMicroservice.Consts
{
    public class SqlCommands {
        public string GET_ALL_MESSAGES() {
            return $"select * from {GeneralConsts.MESSAGE_TABLE}";
        }

        public string CREATE_MESSAGE(Message m)
        {
            var s = m.sender;
            return $"insert into {GeneralConsts.MESSAGE_TABLE} (uuid, content, dateTime, senderUUID, senderName, senderSurname) output inserted.* " +
                $"values ('{m.uuid}', '{m.content}', '{m.dateTime}', '{s.uuid}', '{s.name}', '{s.surname}');";
        }

        public string UPDATE_MESSAGE(Message m)
        {
            var s = m.sender;
            return $"update {GeneralConsts.MESSAGE_TABLE} " +
                $"set content = '{m.content}', " +
                $"dateTime = '{m.dateTime}', " +
                $"senderUUID = '{s.uuid}', " +
                $"senderName = '{s.name}', " +
                $"senderSurname = '{s.surname}' " +
                $"output inserted.* " +
                $"where uuid = '{m.uuid}';";
        }

        public string DELETE_MESSAGE(string uuid)
        {
            return $"delete from {GeneralConsts.MESSAGE_TABLE} where uuid = '{uuid}';";
        }
        public string DELETE_RECIPIENT_BY_MESSAGE(string messageUUID)
        {
            return $"delete from {GeneralConsts.RECIPIENT_TABLE} where messageUUID = '{messageUUID}';";
        }

        public string DELETE_FILE_MESSAGE_BY_MESSAGE(string messageUUID)
        {
            return $"delete from {GeneralConsts.FILE_MESSAGE_TABLE} where messageUUID = '{messageUUID}';";
        }

        public string GET_MESSAGE_BY_UUID(string uuid)
        {
            return $"select * from {GeneralConsts.MESSAGE_TABLE} where uuid = '{uuid}';";
        }

        public string GET_MESSAGES_BY_RECIPIENT(string[] recipients, string sender)
        {
            string inStr = "";
            foreach(string s in recipients)
            {
                inStr += $"'{s}',";
            }
            inStr = inStr.Substring(0, inStr.Length - 1);

            return $"select id, uuid, content, senderName, senderUUID, senderSurname, dateTime " +
                $"from {GeneralConsts.MESSAGE_TABLE} m " +
                $"inner join {GeneralConsts.RECIPIENT_TABLE} r on r.messageUUID = m.uuid " +
                $"where r.messageUUID in (" +
                        $"select messageUUID from {GeneralConsts.RECIPIENT_TABLE} " +
                        $"where recipientUUID in ({inStr}) " +
                        $"group by messageUUID " +
                        $"having COUNT(*) = {recipients.Length}" +
                $") " +
                $"and m.senderUUID = '{sender}' " +
                $"and recipientUUID in ({inStr}) " +
                $"group by id, uuid, content, senderName, senderUUID, senderSurname, dateTime " +
                $"order by dateTime asc";
        }

        public string CREATE_RECIPIENT(Recipient r)
        {
            var re = r.recipient;
            return $"insert into {GeneralConsts.RECIPIENT_TABLE} (messageUUID, recipientUUID, recipientName, recipientSurname) output inserted.* " +
                $"values ('{r.messageUUID}', '{re.uuid}', '{re.name}', '{re.surname}');";
        }
        
        public string UPDATE_RECIPIENT(Recipient r)
        {
            var re = r.recipient;
            return $"update {GeneralConsts.RECIPIENT_TABLE} " +
                $"set messageUUID = '{r.messageUUID}', " +
                $"recipientUUID = '{re.uuid}', " +
                $"recipientName = '{re.name}', " +
                $"recipientSurname = '{re.surname}' " +
                $"output inserted.* " +
                $"where uuid = '{r.uuid}';";
        }

        public string CREATE_FILE_MESSAGE(FileMessage fm)
        {
            var f = fm.file;
            return $"insert into {GeneralConsts.FILE_MESSAGE_TABLE} (messageUUID, fileUUID, filePath) output inserted.* " +
                $"values ('{fm.messageUUID}', '{f.uuid}', '{f.filePath}');";
        }

        public string GET_RECIPIENTS_BY_MESSAGE(string uuid)
        {
            return $"select recipientUUID, recipientName, recipientSurname from {GeneralConsts.RECIPIENT_TABLE} where messageUUID = '{uuid}'";
        }

        internal string GET_FILES_BY_MESSAGE(string uuid)
        {
            return $"select fileUUID, filePath from {GeneralConsts.FILE_MESSAGE_TABLE} where messageUUID = '{uuid}'";
        }
    }
}
