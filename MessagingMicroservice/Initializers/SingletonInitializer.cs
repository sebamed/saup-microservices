using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MessagingMicroservice.Mappers;
using MessagingMicroservice.Services;
using MessagingMicroservice.Services.Implementation;
using MessagingtMicroservice.Services;
using MessagingMicroservice.Consts;
using Commons.HttpClientRequests;
using System.Net.Http;

namespace MessagingMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            services.AddSingleton<IMessagingService, MessagingService>();
            services.AddHttpContextAccessor();
            services.AddSingleton<HttpClientService>();
            services.AddSingleton<HttpClient>();
            // register QueryExecutor singleton
            services.AddSingleton<QueryExecutor>();

            // register ModelMapper singleton
            services.AddSingleton<ModelMapper>();

            // register SqlCommands singleton
            services.AddSingleton<SqlCommands>();
        }
    }
}
