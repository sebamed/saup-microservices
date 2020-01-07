using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectMicroservice.Domain;

namespace SubjectMicroservice.Consts
{
	public class SqlCommands
	{
		public string GET_ALL_SUBJECTS()
		{
			return $"select * from {GeneralConsts.SCHEMA_NAME}.Subject";
		}

        public string GET_SUBJECT_BY_UUID(string uuid)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Subject where uuid = '{uuid}'";
        }

        public string GET_SUBJECTS_BY_NAME(string name)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Subject where name = '{name}'";
        }

        public string CREATE_SUBJECT(Subject subject)
        {
            return $"insert into {GeneralConsts.SCHEMA_NAME}.Subject (uuid, name, description, creationDate) output inserted.* " +
                $"values ('{subject.uuid}', '{subject.name}', '{subject.description}', '{subject.creationDate}');";
        }

        public string UPDATE_SUBJECT(Subject subject)
        {
            return $"update {GeneralConsts.SCHEMA_NAME}.Subject " +
                $"set uuid = '{subject.uuid}', name = '{subject.name}', description = '{subject.description}', creationDate = '{subject.creationDate}' output inserted.* " +
                $"where uuid = '{subject.uuid}';";
        }

        public string DELETE_SUBJECT(string uuid)
        {
            return $"delete from {GeneralConsts.SCHEMA_NAME}.Subject " +
                $"where uuid = '{uuid}';";
        }

		public string GET_ALL_SUBJECT_ARCHIVES()
		{
			return $"select * from {GeneralConsts.SCHEMA_NAME}.SubjectArchive";
		}
		public string GET_SUBJECT_ARHIVE_BY_UUID(string subjectUUID)
		{
			return $"select * from {GeneralConsts.SCHEMA_NAME}.SubjectArchive where subjectUUID = '{subjectUUID}'";
		}
		public string GET_SUBJECT_ARHIVES_BY_NAME(string name)
		{
			return $"select * from {GeneralConsts.SCHEMA_NAME}.SubjectArchive where name = '{name}'";
		}

		public string CREATE_SUBJECT_ARHIVE(SubjectArchive SubjectArchive)
		{
			return $"insert into {GeneralConsts.SCHEMA_NAME}.SubjectArchive (subjectUUID, name, description, creationDate, changeDate, version) output inserted.* " +
				$"values ('{SubjectArchive.subjectUUID}', '{SubjectArchive.name}', '{SubjectArchive.description}', '{SubjectArchive.creationDate}', '{SubjectArchive.changeDate}', '{SubjectArchive.version}');";
		}

		public string UPDATE_SUBJECT_ARHIVE(SubjectArchive SubjectArchive)
		{
			return $"update {GeneralConsts.SCHEMA_NAME}.SubjectArchive " +
				$"set subjectUUID = '{SubjectArchive.subjectUUID}', name = '{SubjectArchive.name}', description = '{SubjectArchive.description}', changeDate = '{SubjectArchive.changeDate}', version = '{SubjectArchive.version}' output inserted.* " +
				$"where subjectUUID = '{SubjectArchive.subjectUUID}';";
		}

		public string DELETE_SUBJECT_ARHIVE(string subjectUUID)
		{
			return $"delete from {GeneralConsts.SCHEMA_NAME}.SubjectArchive " +
				$"where subjectUUID = '{subjectUUID}';";
		}
	}
}

