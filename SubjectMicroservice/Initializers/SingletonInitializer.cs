using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubjectMicroservice.Mappers;
using SubjectMicroservice.Services;
using SubjectMicroservice.Services.Implementation;
using SubjectMicroservice.Consts;
using System.Net.Http;
using Commons.HttpClientRequests;

namespace SubjectMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // register SubjectService singleton
            services.AddSingleton<ISubjectService, SubjectService>();
            services.AddSingleton<ISubjectArchiveService, SubjectArchiveService>();
            services.AddSingleton<HttpClientService>();
            services.AddSingleton<HttpClient>();
            services.AddHttpContextAccessor();

            // register QueryExecutor singleton
            services.AddSingleton<QueryExecutor>();

            // register ModelMapper singleton
            services.AddSingleton<ModelMapper>();

            // register SqlCommands singleton
            services.AddSingleton<SqlCommands>();
        }
    }
}
