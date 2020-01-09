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

        public string GET_SUBJECTS_BY_DEPARTMENT_UUID(string departmentUUID)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Subject where departmentUUID = '{departmentUUID}'";
        }

        public string GET_SUBJECTS_BY_CREATOR_UUID(string creatortUUID)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Subject where creatorUUID = '{creatortUUID}'";
        }

        public string CREATE_SUBJECT(Subject subject)
        {
            return $"insert into {GeneralConsts.SCHEMA_NAME}.Subject (uuid, name, description, creationDate, departmentUUID, creatorUUID) output inserted.* " +
                $"values ('{subject.uuid}', '{subject.name}', '{subject.description}', '{subject.creationDate}', '{subject.department.uuid}', '{subject.creator.uuid}');";
        }

        public string UPDATE_SUBJECT(Subject subject)
        {
            return $"update {GeneralConsts.SCHEMA_NAME}.Subject " +
                $"set uuid = '{subject.uuid}', name = '{subject.name}', description = '{subject.description}', " +
                $"creationDate = '{subject.creationDate}', departmentUUID = '{subject.department.uuid}', creatorUUID = '{subject.creator.uuid}' output inserted.* " +
                $"where uuid = '{subject.uuid}';";
        }

        public string DELETE_SUBJECT(string uuid)
        {
            return $"delete from {GeneralConsts.SCHEMA_NAME}.Subject " +
                $"where uuid = '{uuid}';";
        }

        //ARCHIVE
		public string GET_ALL_ARCHIVES_BY_SUBJECT_UUID(string subjectUUID) {
			return $"select * from {GeneralConsts.SCHEMA_NAME}.SubjectArchive where subjectUUID = '{subjectUUID}'";
		}

        public string GET_LATEST_ARCHIVE_BY_SUBJECT_UUID(string subjectUUID) {
            return $"select top 1 * from {GeneralConsts.SCHEMA_NAME}.SubjectArchive where subjectUUID = '{subjectUUID}' order by version desc";
        }

        public string CREATE_SUBJECT_ARHIVE(SubjectArchive SubjectArchive)
		{
			return $"insert into {GeneralConsts.SCHEMA_NAME}.SubjectArchive (subjectUUID, name, description, creationDate, departmentUUID, creatorUUID, moderatorUUID, changeDate) output inserted.* " +
				$"values ('{SubjectArchive.subjectUUID}', '{SubjectArchive.name}', '{SubjectArchive.description}', " +
                $"'{SubjectArchive.creationDate}', '{SubjectArchive.department.uuid}', '{SubjectArchive.creator.uuid}', " +
                $"'{SubjectArchive.moderator.uuid}', '{SubjectArchive.changeDate}');";
		}

        public string DELETE_ARCHIVES_BY_SUBJECT_UUID(string subjectUUID) {
            return $"delete from {GeneralConsts.SCHEMA_NAME}.SubjectArchive where subjectUUID = '{subjectUUID}'";
        }
    }
}

