using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LectureMaterialMicroservice.Domain;
using SectionMicroservice.Domain;
namespace LectureMaterialMicroservice.Mappers
{
    public class ModelMapper
    {

        public List<Section> MapToSections(IDataReader reader)
        {
            List<Section> sections = new List<Section>();

            while (reader.Read())
            {
                sections.Add(new Section()
                {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    visible = Convert.ToInt32(reader["visible"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    courseUUID = Convert.ToString(reader["courseUUID"])
                });
            }

            return sections;
        }

        public Section MapToSection(IDataReader reader)
        {
            while (reader.Read())
            {
                return new Section()
                {
                    id = Convert.ToInt32(reader["id"]),
                    uuid = Convert.ToString(reader["uuid"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    visible = Convert.ToInt32(reader["visible"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    courseUUID = Convert.ToString(reader["courseUUID"])
                };
            }

            return null;
        }

        public SectionArchive MapToSectionArchive(IDataReader reader)
        {
            while (reader.Read())
            {
                return new SectionArchive()
                {
                    sectionUUID = Convert.ToString(reader["sectionUUID"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    visible = Convert.ToInt32(reader["visible"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    courseUUID = Convert.ToString(reader["courseUUID"]),
                    moderatorUUID = Convert.ToString(reader["moderatorUUID"]),
                    changeDate = Convert.ToDateTime(reader["changeDate"]),
                    version = Convert.ToInt32(reader["version"])
                };
            }

            return null;
        }

        public List<SectionArchive> MapToSectionArchives(IDataReader reader)
        {
            List<SectionArchive> sectionArchives = new List<SectionArchive>();

            while (reader.Read())
            {
                sectionArchives.Add(new SectionArchive()
                {
                    sectionUUID = Convert.ToString(reader["sectionUUID"]),
                    name = Convert.ToString(reader["name"]),
                    description = Convert.ToString(reader["description"]),
                    visible = Convert.ToInt32(reader["visible"]),
                    creationDate = Convert.ToDateTime(reader["creationDate"]),
                    courseUUID = Convert.ToString(reader["courseUUID"]),
                    moderatorUUID = Convert.ToString(reader["moderatorUUID"]),
                    changeDate = Convert.ToDateTime(reader["changeDate"]),
                    version = Convert.ToInt32(reader["version"])
                });
            }

            return sectionArchives;
        }
    }
}
