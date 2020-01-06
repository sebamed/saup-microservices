using System;
using MailService.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailService.Initializers {
    public class ConfigurationInitializer : IInitializer {
        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {
            services.Configure<EmailSenderConfigurationModel>(configuration.GetSection("Email"));
        }
    }
}
