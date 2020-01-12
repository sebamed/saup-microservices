using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMicroservice.Domain;

namespace TeamMicroservice.Consts
{
    public class SqlCommands
    {
        public string GET_ALL_TEAMS()
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Team";
        }

        public string GET_TEAM_BY_UUID(string uuid)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Team where uuid = '{uuid}'";
        }

        public string GET_TEAM_BY_NAME_AND_COURSE(string name, string courseUUID)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Team where name = '{name}' and courseUUID = '{courseUUID}'";
        }

        public string GET_TEAM_BY_NAME(string name)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Team where name = '{name}'";
        }

        public string GET_TEAMS_BY_COURSE(string courseUUID)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Team where courseUUID = '{courseUUID}'";
        }

        public string CREATE_TEAM(Team team)
        {
            return $"insert into {GeneralConsts.SCHEMA_NAME}.Team (uuid, name, description, teacherUUID, courseUUID) output inserted.* " +
                $"values ('{team.uuid}', '{team.name}', '{team.description}', '{team.teacher.uuid}', '{team.course.uuid}');";
        }

        public string UPDATE_TEAM(Team team)
        {
            return $"update {GeneralConsts.SCHEMA_NAME}.Team " +
                $"set name = '{team.name}', description = '{team.description}', teacherUUID = '{team.teacher.uuid}', courseUUID = '{team.course.uuid}' output inserted.* " +
                $"where uuid = '{team.uuid}';";
        }

        public string DELETE_TEAM(string uuid)
        {
            return $"delete from {GeneralConsts.SCHEMA_NAME}.Team " +
                $"where uuid = '{uuid}';";
        }

        public string ADD_STUDENT_INTO_TEAM(StudentTeam studentTeam)
        {
            return $"insert into {GeneralConsts.SCHEMA_NAME}.StudentCourseTeam (teamUUID, courseUUID, studentUUID) output inserted.* " +
                $"values ('{studentTeam.team.uuid}', '{studentTeam.team.course.uuid}', '{studentTeam.student.uuid}');";
        }

        public string DELETE_STUDENT_FROM_TEAM(string studentUUID, string teamUUID)
        {
            return $"delete from {GeneralConsts.SCHEMA_NAME}.StudentCourseTeam " +
                $"where teamUUID = '{teamUUID}' and studentUUID = '{studentUUID}';";
        }

        public string GET_ONE_BY_TEAM_AND_STUDENT(string studentUUID, string teamUUID)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.StudentCourseTeam where studentUUID = '{studentUUID}' and teamUUID = '{teamUUID}'";
        }
    }
}
