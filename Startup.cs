using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TodoApi.Models;

namespace TodoApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<TodoContext> (opt => opt.UseInMemoryDatabase ("ToDoList"));
            services.AddMvc ();

            //add swagger
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info {
                    Version = "v1",
                        Title = "My API",
                        Description = "A simple example ASP.NET Core Web API",
                        TermsOfService = "None",
                        Contact = new Contact {
                            Name = "Giraudev", Email = "",
                                Url = "http://github.com/giraudev"
                        }
                });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine (basePath, "TodoApi.xml");
                c.IncludeXmlComments (xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

app.UseStaticFiles();
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc ();
        }
    }
}