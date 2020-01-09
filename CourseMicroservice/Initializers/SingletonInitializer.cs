﻿using CourseMicroservice.Mappers;
using CourseMicroservice.Services;
using CourseMicroservice.Services.Implementation;
using CourseMicroservice.Consts;
using System.Net.Http;
using Commons.HttpClientRequests;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // register services
            services.AddSingleton<ICourseService, CourseService>();
            services.AddSingleton<HttpClientService>();
            services.AddSingleton<HttpClient>();

            // register QueryExecutor singleton
            services.AddSingleton<QueryExecutor>();

            // register ModelMapper singleton
            services.AddSingleton<ModelMapper>();

            //register SqlCommands singleton
            services.AddSingleton<SqlCommands>();
        }
    }
}
