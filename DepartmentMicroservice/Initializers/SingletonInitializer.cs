using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.DatabaseUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DepartmentMicroservice.Mappers;
using DepartmentMicroservice.Services;
using DepartmentMicroservice.Services.Implementation;
using DepartmentMicroservice.Consts;

namespace DepartmentMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // register services
            services.AddSingleton<IFacultyService, FacultyService>();

            // register QueryExecutor singleton
            services.AddSingleton<QueryExecutor>();

            // register ModelMapper singleton
            services.AddSingleton<ModelMapper>();

            // register SqlCommands singleton
            services.AddSingleton<SqlCommands>();
        }
    }
}
