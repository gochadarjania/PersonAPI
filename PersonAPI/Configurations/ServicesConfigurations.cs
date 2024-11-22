using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonAPI.Core;
using PersonAPI.Core.AutoMapper;
using PersonAPI.Core.Contracts;
using PersonAPI.Core.Contracts.Repositories;
using PersonAPI.Core.Services;
using PersonAPI.Infrastructure;
using PersonAPI.Infrastructure.Repositories;

namespace PersonAPI.Configurations
{
    public static class ServicesConfigurations
    {
        public static void RegisterServices(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddDbContext<PersonDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            service.AddTransient<IPersonService, PersonService>();

            service.AddTransient<IPersonRepository, PersonRepository>();
            service.AddTransient<IRelatedPersonRepository, RelatedPersonRepository>();
            service.AddHttpClient<IFileService, FileService>();

            service.AddTransient<IUnitOfWork, UnitOfWork>();

            #region Auto Mapper Config
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            service.AddSingleton(mapper);
            #endregion

            service.AddSingleton<IConfiguration>(configuration);
        }
    }
}
