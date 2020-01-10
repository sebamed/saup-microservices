using FileMicroservice.Consts;
using FileMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileMicroservice.Consts {
    public class SqlCommands {
        public string GET_ALL_FILES() {
            return $"select * from {GeneralConsts.FILE_TABLE}";
        }

        public string CREATE_FILE(File file)
        {
            return $"insert into {GeneralConsts.FILE_TABLE} (uuid, path, fileTypeUUID) output inserted.* " +
               $"values ('{file.uuid}', '{file.filePath}', 'aadcd643-d85b-4240-9b02-d861f5100e4e');";
        }

        public string GET_FILE_BY_UUID(string uuid)
        {
            return $"select * from {GeneralConsts.FILE_TABLE} where uuid = '{uuid}';";
        }

        public string GET_FILE_BY_PATH(string path)
        {
            return $"select * from {GeneralConsts.FILE_TABLE} where path = '{path}';";
        }

        public string DELETE_FILE(string uuid)
        {
            return $"delete from {GeneralConsts.FILE_TABLE} where uuid = '{uuid}';";
        }

        internal string UPDATE_FILE(File file)
        {
            return $"update {GeneralConsts.FILE_TABLE} " +
                $"set uuid = '{file.uuid}', path = '{file.filePath}' " +
                $"output inserted.* " +
                $"where uuid = '{file.uuid}';";
        }
    }
}
