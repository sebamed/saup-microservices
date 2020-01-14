using LectureMaterialMicroservice.Domain;
using SectionMicroservice.Domain;
using SectionMicroservice.Domain.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Consts {
    public class SqlCommands {

        //SQL commands for SECTION

        public string GET_ALL_SECTIONS()
        {
            return $"select s.* " +
                $"from SAUP_SECTION.Section s ";
        }

        public string GET_ONE_SECTION_BY_UUID(string uuid)
        {
            return $"select s.*" +
                $"from SAUP_SECTION.Section s where s.uuid = '{uuid}';";
        }

        public string GET_ONE_SECTION_BY_NAME_AND_COURSE(string name, string courseUUID)
        {
            return $"select s.* from SAUP_SECTION.Section s where s.name = '{name}' and s.courseUUID = '{courseUUID}';";
        }

        public string GET_VISIBLE_SECTIONS()
        {
            return $"select s.* " +
               $"from SAUP_SECTION.Section s where s.visible = 1";
        }

        public string GET_SECTIONS_BY_COURSE(string courseUUID, bool visible)
        {
            if(visible)
            return $"select s.* " +
               $"from SAUP_SECTION.Section s where s.courseUUID = '{courseUUID}' and s.visible = 1";
            else
                return $"select s.* " +
               $"from SAUP_SECTION.Section s where s.courseUUID = '{courseUUID}'";
        }

        public string CREATE_SECTION(Section section) {
            return $"insert into SAUP_SECTION.Section (uuid, name, description, visible, creationDate, courseUUID) output inserted.* " +
               $"values ('{section.uuid}', '{section.name}', '{section.description}', '{section.visible}', '{section.creationDate}', '{section.course.uuid}');";
        }

        public string UPDATE_SECTION(Section section)
        {
            return $"update SAUP_SECTION.Section " +
               $"set name = '{section.name}', description = '{section.description}', visible = '{section.visible}', creationDate = '{section.creationDate}', courseUUID = '{section.course.uuid}' output inserted.* " +
               $"where uuid = '{section.uuid}';";
        }

        public string DELETE_SECTION(string uuid) {
            return $"delete from SAUP_SECTION.Section " +
                $"where uuid = '{uuid}';";
        }

        //SQL commands for SECTION_ARCHIVE
        public string GET_ALL_ARCHIVES_BY_SECTION_UUID(string sectionUUID)
        {
            return $"select *" +
               $"from SAUP_SECTION.SectionArchive where sectionUUID = '{sectionUUID}' order by version desc;";
        }

        public string GET_LATEST_ARCHIVE_BY_SECTION_UUID(string sectionUUID)
        {
            return $"select top 1 * from SAUP_SECTION.SectionArchive where sectionUUID = '{sectionUUID}' order by version desc";
        }

        public string CREATE_ARCHIVE(SectionArchive sectionArchive) {
            return $"insert into SAUP_SECTION.SectionArchive (sectionUUID, name, description, visible, creationDate, courseUUID, moderatorUUID, changeDate) " +
               $"values ('{sectionArchive.sectionUUID}','{sectionArchive.name}', '{sectionArchive.description}', '{sectionArchive.visible}', '{sectionArchive.creationDate}', '{sectionArchive.course.uuid}', '{sectionArchive.moderator.uuid}', '{sectionArchive.changeDate}');";
        }

        public string DELETE_ARCHIVES_BY_SECTION_UUID(string sectionUUID)
        {
            return $"delete from SAUP_SECTION.SectionArchive where sectionUUID = '{sectionUUID}'";
        }

        //SQL commands for MATERIAL
        public string GET_ONE_FILE_BY_SECTION_AND_FILE_UUID(string sectionUUID, string fileUUID)
        {
            return $"select * from SAUP_SECTION.Material where sectionUUID = '{sectionUUID}' and fileUUID = '{fileUUID}';";
        }

        public string GET_ONE_FILE_BY_FILE_UUID(string fileUUID)
        {
            return $"select * from SAUP_SECTION.Material where fileUUID = '{fileUUID}';";
        }

        public string GET_FILES_BY_SECTION(string sectionUUID) {
            return $"select * from SAUP_SECTION.Material where sectionUUID = '{sectionUUID}';";
        }

        public string ADD_FILE_TO_SECTION(Material material)
        {
            return $"insert into SAUP_SECTION.Material (sectionUUID, fileUUID, filePath, visible) output inserted.* " +
                $"values ('{material.section.uuid}', '{material.file.uuid}', '{material.file.filePath}', {material.visible});";
        }

        public string UPDATE_FILE_IN_MATERIAL(File file)
        {
            return $"update SAUP_SECTION.Material " +
              $"set filePath = '{file.filePath}' output inserted.* where fileUUID = '{file.uuid}';";
        }

        public string DELETE_FILE_FROM_SECTION(string sectionUUID, string fileUUID)
        {
            return $"delete from SAUP_SECTION.Material where sectionUUID = '{sectionUUID}' and fileUUID = '{fileUUID}'";
        }
    }

}
