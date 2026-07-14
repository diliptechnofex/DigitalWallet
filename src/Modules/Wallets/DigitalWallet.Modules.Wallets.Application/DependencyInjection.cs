using DigitalWallet.Modules.Wallets.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DigitalWallet.Modules.Wallets.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWalletsApplication(this IServiceCollection services)
        {
            services.TryAddSingleton(TimeProvider.System);

            services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: false);

            services.AddLogging();

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(AssemblyReference.Assembly);

                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

                var licenseKey = Environment.GetEnvironmentVariable("MEDIATR_LICENSE_KEY");

                if (!string.IsNullOrWhiteSpace(licenseKey))
                {
                    configuration.LicenseKey = licenseKey;
                }
            });

            return services;
        }
    }
}
