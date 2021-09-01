using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenOpinions.Data;
using OpenOpinions.Models;
using OpenOpinions.Profiles;

namespace OpenOpinions
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
            string dataSource = Configuration.GetValue<string>("DataSource:Current");

            if (dataSource.Equals("SQLLite"))
            {
                services.AddDbContext<OpinionContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

                services.AddScoped<IOpinionRepository, SqlOpinionRepository>();

            }
            else if (dataSource.Equals("LiteDb"))
            {
                services.AddSingleton<DbLiteOpinionContext>();
                services.AddScoped<IOpinionRepository, DbLiteOpinionRepository>();
            }
            else
            {
                services.AddSingleton<IOpinionRepository, InMemoryOpinionRepository>();
            }
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenOpinions", Version = "v1" });
            });
            services.AddAutoMapper(typeof(OpinionsProfiles));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
