using MailService.Providers;
using MailService.Services;
using MailService.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MailService.Initializers {

    /**
     * Class that initializes singletons neccesary for DI
     */
    public class SingletonInitializer : IInitializer {

        public void InitializeServices(IServiceCollection services, IConfiguration configuration) {

            // register services
            services.AddSingleton<IMailService, EmailService>();

            // register SMTP Client Provider
            services.AddSingleton<SmtpClientProvider>();

        }
    }
}
