using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileMicroservice.Mappers;
using FileMicroservice.Services;
using FileMicroservice.Services.Implementation;
using FileMicroservice.Consts;
using Commons.HttpClientRequests;
using System.Net.Http;

namespace FileMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // register UserService singleton
            services.AddSingleton<IFileService, FileService>();
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
