using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddEncryptKey(services, configuration);
            AddTokenJWT(services, configuration);
            services.AddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
        }

        private static void AddEncryptKey(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetRequiredSection("Configuration:_encryptKey");
            services.AddScoped(option => new PasswordEncrypt(section.Value));
        }
        
        private static void AddTokenJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionLifeTime = configuration.GetRequiredSection("Configuration:_lifeTimeInMinutes");
            var sectionSecureKey = configuration.GetRequiredSection("Configuration:_secureKey");

            services.AddScoped(option => new TokenController(int.Parse(sectionLifeTime.Value), sectionSecureKey.Value));
        }
    }
}
