using AutoMapper;
using MessagingMicroservice.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingMicroservice.Initializers
{
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
