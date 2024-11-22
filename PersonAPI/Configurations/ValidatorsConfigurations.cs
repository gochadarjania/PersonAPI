using FluentValidation;
using FluentValidation.AspNetCore;
using PersonAPI.Core.Models;
using PersonAPI.Core.Validators;

namespace PersonAPI.Configurations
{
    public static class ValidatorsConfigurations
    {
        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IValidator<PersonModel>, PersonValidator>();
            services.AddTransient<IValidator<PhoneNumberModel>, PhoneNumberValidator>();
            services.AddTransient<IValidator<RelatedPersonModel>, RelatedPersonValidator>();

            services.AddTransient<PersonValidator>();
            services.AddTransient<PhoneNumberValidator>();
            services.AddTransient<RelatedPersonValidator>();

            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });
            services.AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
