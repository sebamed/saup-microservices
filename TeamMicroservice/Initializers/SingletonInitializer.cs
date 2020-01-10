using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamMicroservice.Mappers;
using TeamMicroservice.Services;
using TeamMicroservice.Services.Implementation;
using TeamMicroservice.Consts;
using Commons.HttpClientRequests;
using System.Net.Http;

namespace TeamMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // register UserService singleton
            services.AddSingleton<ITeamService, TeamService>();
            services.AddSingleton<IStudentTeamService, StudentTeamService>();
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
