using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace TFTIC.BISP.Math.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().
                AddXmlSerializerFormatters();

            services.AddMvc();
            services.AddSwaggerGen(
                doc => {
                    doc.SwaggerDoc(
                        "MathFacile",
                        new OpenApiInfo
                        {
                            Title="Mathématique facile",
                            Description="Service web permettant d'apprendre les math d'une autre manière...",
                            Version="v1.0",
                            Contact = new OpenApiContact
                            {
                                Name = "Legrain Samuel",
                                Email = "samuel.legrain@bstorm.be",
                                Url = new Uri("https://github.com/SamBstorm/")
                            }
                        }
                        );
                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    s => { s.SwaggerEndpoint("MathFacile/swagger.json", "Mathématique facile - v1.0"); }
                    );
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
