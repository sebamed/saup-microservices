using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DepartmentMicroservice.Mappers;

namespace DepartmentMicroservice.Initializers {
    public class AutoMapperInitializer : IInitializer {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            var mapperConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
