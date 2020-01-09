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

        public string GET_TEAMS_BY_NAME(string name)
        {
            return $"select * from {GeneralConsts.SCHEMA_NAME}.Team where name = '{name}'";
        }

        public string CREATE_TEAM(Team team)
        {
            return $"insert into {GeneralConsts.SCHEMA_NAME}.Team (uuid, name, description) output inserted.* " +
                $"values ('{team.uuid}', '{team.name}', '{team.description}');";
        }

        public string UPDATE_TEAM(Team team)
        {
            return $"update {GeneralConsts.SCHEMA_NAME}.Team " +
                $"set name = '{team.name}', description = '{team.description}' output inserted.* " +
                $"where uuid = '{team.uuid}';";
        }

        public string DELETE_TEAM(string uuid)
        {
            return $"delete from {GeneralConsts.SCHEMA_NAME}.Team " +
                $"where uuid = '{uuid}';";
        }
    }
}
