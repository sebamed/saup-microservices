using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthResource.Consts;
using AuthResource.Mappers;
using AuthResource.Services;
using AuthResource.Services.Implementation;
using AuthResource.Utils;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AuthResource.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            services.AddSingleton<IAuthService, AuthService>();
            services.AddTransient<JwtGenerator>();

            // register QueryExecutor singleton
            services.AddSingleton<QueryExecutor>();

            // register ModelMapper singleton
            services.AddSingleton<ModelMapper>();

            // register SqlCommands singleton
            services.AddSingleton<SqlCommands>();

        }
    }
}
