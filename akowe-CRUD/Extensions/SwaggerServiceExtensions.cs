using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace akowe_CRUD.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Akowe Crud",
                    Version = "v1",
                    TermsOfService = new Uri("https://akowe.app"),
                    Contact = new OpenApiContact
                    {
                        Name = "Akowe App",
                        Email = "olufowobibabs@gmail.com",
                        Url = new Uri("https://akowe.app"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://akowe.app"),
                    }
                });
            });
        }
        public static void UserSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Akowe App";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Akowe-Crud Version 1");
            });
        }
    }
}
