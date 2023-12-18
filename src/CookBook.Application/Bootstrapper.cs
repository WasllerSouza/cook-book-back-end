using CookBook.Application.Services.Cryptography;
using CookBook.Application.Services.Token;
using CookBook.Application.Services.UserSession;
using CookBook.Application.UseCases.Revenue.Register;
using CookBook.Application.UseCases.Token;
using CookBook.Application.UseCases.User.RecoveryPassword;
using CookBook.Application.UseCases.User.Register;
using CookBook.Application.UseCases.User.SingIn;
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
            AddUseCase(services);
            AddUserSession(services);
        }

        private static void AddEncryptKey(IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetRequiredSection("Configuration:EncryptionPassword:_encryptKey");
            services.AddScoped(option => new PasswordEncrypt(section.Value));
        }

        private static void AddTokenJWT(IServiceCollection services, IConfiguration configuration)
        {
            var sectionLifeTime = configuration.GetRequiredSection("Configuration:Jwt:_lifeTimeInMinutes");
            var sectionSecureKey = configuration.GetRequiredSection("Configuration:Jwt:_secureKey");

            services.AddScoped(option => new TokenService(int.Parse(sectionLifeTime.Value), sectionSecureKey.Value));
        }

        private static void AddUseCase(IServiceCollection services)
        {
            services.AddScoped<ISingInUseCase, UserSingInUseCase>()
                    .AddScoped<IRecoveryPasswordUseCase, RecoveryPasswordUseCase>()
                    .AddScoped<IUserRegisterUseCase, UserRegisterUseCase>()
                    .AddScoped<IRevenueRegisterUseCase, RevenueRegisterUseCase>()
                    .AddScoped<ITokenUseCase, TokenUseCase>()
                    ;
        }

        private static void AddUserSession(IServiceCollection services)
        {
            services.AddScoped<IUserSession, UserSession>();
        }
    }
}
