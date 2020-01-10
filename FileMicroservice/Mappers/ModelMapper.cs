using FileMicroservice.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace FileMicroservice.Mappers
{
	public class ModelMapper {

		public List<File> MapToFiles(IDataReader reader)
		{
			List<File> files = new List<File>();

			while (reader.Read())
			{
				files.Add(new File()
				{
					id = Convert.ToInt32(reader["id"]),
					uuid = Convert.ToString(reader["uuid"]),
					filePath = Convert.ToString(reader["path"])
				});
			}

			return files;
		}

		public File MapToFile(IDataReader reader)
		{
			while (reader.Read())
			{
				return new File()
				{
					id = Convert.ToInt32(reader["id"]),
					uuid = Convert.ToString(reader["uuid"]),
					filePath = Convert.ToString(reader["path"]),
                };
			}
			return null;
		}

		public File MapToFileAfterInsert(IDataReader reader)
		{
			reader.Read();
			return new File()
			{
				id = Convert.ToInt32(reader["id"]),
				uuid = Convert.ToString(reader["uuid"]),
				filePath = Convert.ToString(reader["path"]),
            };
		}
	}
}
