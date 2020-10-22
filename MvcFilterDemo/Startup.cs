using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcFilterDemo.Filters;

namespace MvcFilterDemo
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
            services.AddControllers(config=> {
                // ȫ��ע����Ȩ������
                //config.Filters.Add(typeof(MyAuthorizeFilter));
                // ȫ��ע����Դ������
                config.Filters.Add(typeof(MyResourceFilter));
                // ȫ��ע���쳣������
                config.Filters.Add(typeof(MyExceptionFilter));
                // ȫ��ע��Action������
                config.Filters.Add(typeof(MyActionFilter),3);
                //config.Filters.Add(typeof(MyNoAttributeActionFilter), 3);
                // ȫ��ע��Result������
                config.Filters.Add(typeof(MyResultFilter));
            });
            services.AddTransient(typeof(MyNoAttributeActionFilter));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
