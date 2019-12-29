using AutoMapper;
using AutoMapper.Data;
using Commons.ExceptionHandling.Exceptions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Commons.DatabaseUtils {
    public class QueryExecutor {

        public List<T> Execute<T>(string connectionUrl, string query) {
            using (SqlConnection connection = new SqlConnection(connectionUrl)) {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                try {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    // map ${reader} result to actual list of objects
                    if (reader.HasRows) {
                        var configuration = new MapperConfiguration(cfg => {
                            cfg.AddDataReaderMapping();
                            cfg.CreateMap<IDataReader, T>();
                        });

                        Mapper mapper = new Mapper(configuration);

                        List<T> data = mapper.Map<List<T>>(reader);

                        connection.Close();

                        return data;
                    }

                } catch(BaseException e) {

                }
                
                connection.Close();
                return null;
            }
        }
    }

    
}
