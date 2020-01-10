using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LectureMaterialMicroservice.Mappers;
using LectureMaterialMicroservice.Services;
using LectureMaterialMicroservice.Services.Implementation;
using LectureMaterialMicroservice.Consts;
using SectionMicroservice.Services;
using SectionMicroservice.Services.Implementation;
using System.Net.Http;
using Commons.HttpClientRequests;

namespace LectureMaterialMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // register services
            services.AddSingleton<ISectionService, SectionService>();
            services.AddSingleton<ISectionArchiveService, SectionArchiveService>();
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
