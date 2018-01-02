using loginRegistration.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace loginRegistration
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IHostingEnvironment env){
            var builder=new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
                Configuration=builder.Build();
    
        }
        public void ConfigureServices(IServiceCollection services){
            services.AddDbContext<LoginRegContext>(options => options.UseMySql(Configuration["DbInfo:ConnectionString"]));
            services.AddMvc();
            services.AddSession();
            
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseSession();
            app.UseMvc();
          
        }
    }
}
