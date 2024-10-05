using Microsoft.EntityFrameworkCore;
using SecretsService.Service.Contracts;
using Microsoft.OpenApi.Models;
using SecretsService.API.Middlewares.Exceptions;
using SecretsService.Model.Context;

namespace SecretsService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SecretsService API", Version = "v1" });
            });

            services.AddScoped<ISecretsService, Service.Services.SecretsService>();
            services.AddDataProtection();
            services.AddScoped<ISecretsDataProtector, Service.Services.SecretsDataProtector>();

            services.AddDbContext<SecretsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecretsService API V1"));

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}