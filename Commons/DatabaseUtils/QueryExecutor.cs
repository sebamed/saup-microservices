﻿using AutoMapper;
using AutoMapper.Data;
using Commons.ExceptionHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Commons.DatabaseUtils {

    public class QueryExecutor {

        public T Execute<T>(string connectionUrl, string query, Func<IDataReader, T> map) {
            using (SqlConnection connection = new SqlConnection(connectionUrl)) {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                try {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) {
                            try {
                                var data = map(reader);

                                reader.Close();
                                connection.Close();

                                return data;

                            } catch (Exception e) {
                                throw new Exception("501");
                            }

                        } else {
                            connection.Close();
                            return default(T);
                        }
                    
                } catch (Exception e) {
                    if (e.Message.Equals("501")) {
                        throw new MapperDisfuntionException("There was a problem with mapping the result from the db!", "Query Executor - Manual Mapper");
                    } else {
                        throw new BadSqlQueryException($"There was an error with the database while trying to execute: `{query}`", "Query Executor - Manual Mapper");
                    }
                }
            }
        }

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

                        reader.Close();
                        connection.Close();

                        return data;
                    }

                } catch (Exception e) {
                    throw new BadSqlQueryException($"There was an error with the database while trying to execute: `{query}`", "Query Executor - Auto Mapper");
                }

                connection.Close();
                return null;
            }
        }
    }


}
