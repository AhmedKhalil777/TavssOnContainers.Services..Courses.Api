using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Api.Options;
using Identity.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Api.Installers
{
    public class MongoDBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CourseStoreDatabaseSettings>(configuration.GetSection(nameof(CourseStoreDatabaseSettings)));
            services.AddSingleton<ICoursestoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CourseStoreDatabaseSettings>>().Value);
            services.AddSingleton<IMongoCourseService,MongoCourseService>();
        }
    }
}
