using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Commons.DatabaseUtils;
using Commons.HttpClientRequests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserMicroservice.Consts;
using UserMicroservice.Mappers;
using UserMicroservice.Services;
using UserMicroservice.Services.Implementation;

namespace UserMicroservice.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // Register services            
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<IRoleService, RoleService>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddSingleton<ITeacherService, TeacherService>();
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
