﻿using LectureMaterialMicroservice.Domain;
using SectionMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Consts {
    public class SqlCommands {
       
        //SQL commands for Section
        public string CREATE_SECTION(Section section) {
            return $"insert into SAUP_SECTION.Section (uuid, name, description, visible, creationDate, courseUUID) output inserted.* " +
               $"values ('{section.uuid}', '{section.name}', '{section.description}', '{section.visible}', '{section.creationDate}', '{section.courseUUID}');";
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
               $"set name = '{section.name}', description = '{section.description}', visible = '{section.visible}', creationDate = '{section.creationDate}', courseUUID = '{section.courseUUID}' output inserted.* " +
               $"where uuid = '{section.uuid}';";
        }

        //SQL commands for Section_Archive
        public string GET_ALL_ARCHIVES() {
            return $"select sa.* " +
               $"from SAUP_SECTION.SectionArchive sa ";
        }

        public string CREATE_ARCHIVE(SectionArchive sectionArchive) {
            return $"insert into SAUP_SECTION.SectionArchive (sectionUUID, name, description, visible, creationDate, courseUUID, moderatorUUID, changeDate) output inserted.* " +
               $"values ('{sectionArchive.sectionUUID}','{sectionArchive.name}', '{sectionArchive.description}', '{sectionArchive.visible}', '{sectionArchive.creationDate}', '{sectionArchive.courseUUID}', '{sectionArchive.moderatorUUID}', '{sectionArchive.changeDate}');";
        }

        public string GET_ONE_ARCHIVE_BY_SECTION_UUID(string sectionUUID) {
            return $"select sa.*" +
               $"from SAUP_SECTION.SectionArchive sa where sa.sectionUUID = '{sectionUUID}';";
        }

        public string DELETE_ARCHIVE(string sectionUUID) {
            return $"delete from SAUP_SECTION.SectionArchive " +
               $"where SectionUUID = '{sectionUUID}';";
        }

        public string UPDATE_ARCHIVE(SectionArchive sectionArchive)
        {
            return $"update SAUP_SECTION.SectionArchive " +
               $"set sectionUUID = '{sectionArchive.sectionUUID}', name = '{sectionArchive.name}', description = '{sectionArchive.description}', visible = '{sectionArchive.visible}', creationDate = '{sectionArchive.creationDate}', courseUUID = '{sectionArchive.courseUUID}', moderatorUUID = '{sectionArchive.moderatorUUID}', changeDate = '{sectionArchive.changeDate}' output inserted.* " +
               $"where sectionUUID = '{sectionArchive.sectionUUID}';";
        }

        public string GET_VISIBLE_SECTIONS_FROM_ARCHIVE()
        {
            return $"select sa.* " +
               $"from SAUP_SECTION.SectionArchive sa where sa.visible = 1";
        }
    }
}
