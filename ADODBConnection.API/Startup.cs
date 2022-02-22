using ADODBConnection.API.Middlewares;
using ADODBConnection.Contracts.MultiDBConnection;
using ADODBConnection.DAL;
using ADODBConnection.DAL.Services;
using ADODBConnection.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace ADODBConnection
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public delegate IEmployeeDBService EmployeeDBServiceResolver(DBConnectionType dBConnectionType);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ADODBConnection.API",
                    Version = "v1",
                    Description = "This is Version V1",
                });
                options.OperationFilter<AddRequiredHeaderParameters>();
            });

            services.AddDbContextPool<EmployeeContext>(options =>
                options.UseSqlServer(Configuration.GetSection("AppSettings").GetSection("DBConnectionString").Value)
            );

            services.AddScoped<IEmployeeDBService, EmployeeEFDBService>();
            services.AddScoped<IEmployeeDBService, EmployeeADODBService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.TryAddScoped<EmployeeDBServiceResolver>(es => dBConnectionType =>
            {
                return es.GetServices<IEmployeeDBService>().FirstOrDefault(w => w.dBConnectionType == dBConnectionType);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
