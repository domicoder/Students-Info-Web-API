using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PERSISTENCE;
using SERVICES;
using Swashbuckle.AspNetCore.Swagger;

namespace WEB_API
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
            var connection = Configuration.GetConnectionString("connection"); 
            services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(connection));

            services.AddTransient<IStudentService, StudentService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Adding Swagger to API
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Students API",
                    Description = "Students API made by ASP.NET CORE 2.1",
                    Contact = new Contact() {
                           Name = "Yander Sanchez",
                           Email = "ysanchez.business@gmail.com",
                           Url = "https://github.com/zardecs"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(x => {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Students API - v1");
            });
        }
    }
}
