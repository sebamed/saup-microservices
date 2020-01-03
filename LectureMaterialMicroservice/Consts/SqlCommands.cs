using LectureMaterialMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LectureMaterialMicroservice.Consts {
    public class SqlCommands {
       
        public string CREATE_SECTION(Section section) {
            return $"insert into SAUP_SECTION.Section (uuid, name, description, visible, creationDate, courseID) output inserted.* " +
               $"values ('{section.uuid}', '{section.name}', '{section.description}', '{section.visible}', '{section.creationDate}', {1});";
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

        public string DELETE_SECTION_BY_UUID(string uuid) {
            return $"delete from SAUP_SECTION.Section " +
                $"where uuid = '{uuid}';";
        }

        public string GET_ALL_ARCHIVES() {
            return $"select sa.* " +
               $"from SAUP_SECTION.SectionArchive sa ";
        }
    }
}
