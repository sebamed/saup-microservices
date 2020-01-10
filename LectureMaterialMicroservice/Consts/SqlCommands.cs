using LectureMaterialMicroservice.Domain;
using SectionMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Consts {
    public class SqlCommands {
       
        //SQL commands for SECTION
        public string CREATE_SECTION(Section section) {
            return $"insert into SAUP_SECTION.Section (uuid, name, description, visible, creationDate, courseUUID) output inserted.* " +
               $"values ('{section.uuid}', '{section.name}', '{section.description}', '{section.visible}', '{section.creationDate}', '{section.course.uuid}');";
        }
        public string GET_ALL_SECTIONS() {
            return $"select s.* " +
                $"from SAUP_SECTION.Section s ";
        }

        public string GET_ONE_SECTION_BY_UUID(string uuid) {
            return $"select s.*" +
                $"from SAUP_SECTION.Section s where s.uuid = '{uuid}';";
        }

        public string GET_VISIBLE_SECTIONS() {
            return $"select s.* " +
               $"from SAUP_SECTION.Section s where s.visible = 1";
        }

        public string DELETE_SECTION(string uuid) {
            return $"delete from SAUP_SECTION.Section " +
                $"where uuid = '{uuid}';";
        }

        public string UPDATE_SECTION(Section section) {
            return $"update SAUP_SECTION.Section " +
               $"set name = '{section.name}', description = '{section.description}', visible = '{section.visible}', creationDate = '{section.creationDate}', courseUUID = '{section.course.uuid}' output inserted.* " +
               $"where uuid = '{section.uuid}';";
        }

        //SQL commands for SECTION_ARCHIVE

        public string CREATE_ARCHIVE(SectionArchive sectionArchive) {
            return $"insert into SAUP_SECTION.SectionArchive (sectionUUID, name, description, visible, creationDate, courseUUID, moderatorUUID, changeDate) output inserted.* " +
               $"values ('{sectionArchive.sectionUUID}','{sectionArchive.name}', '{sectionArchive.description}', '{sectionArchive.visible}', '{sectionArchive.creationDate}', '{sectionArchive.course.uuid}', '{sectionArchive.moderator.uuid}', '{sectionArchive.changeDate}');";
        }

        public string GET_ALL_ARCHIVES_BY_SECTION_UUID(string sectionUUID) {
            return $"select *" +
               $"from SAUP_SECTION.SectionArchive where sectionUUID = '{sectionUUID}' order by version desc;";
        }

        public string GET_ONE_ARCHIVE_BY_LATEST_VERSION()
        {
            return $"select top 1 *" +
               $"from SAUP_SECTION.SectionArchive order by version desc;";
        }
    }
}
