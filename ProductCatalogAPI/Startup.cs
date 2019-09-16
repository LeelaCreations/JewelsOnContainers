using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductCatalogAPI.Data;

namespace ProductCatalogAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<CatalogContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddSwaggerGen(options=> 
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("V1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Jewels On Container - Product catalog Http API",
                    Version = "V1",
                    Description = "The product catalog API for Jewels",
                    TermsOfService = "TermsOfService"
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

            app.UseSwagger()
                .UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "ProductcatalogAPI V1");
                });
            app.UseMvc();
        }
    }
}
