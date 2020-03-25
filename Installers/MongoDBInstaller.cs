using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Options;
using Course.Api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Course.Api.Installers
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
