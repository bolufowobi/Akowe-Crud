using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using akowe_CRUD.Services;
using Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace akowe_CRUD.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAcademicCredentialService, AcademicCredentialService>();
        }


    }
}
