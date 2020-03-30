using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sum.Api.ServiceExtension;
using Sum.Core.HealthCheck;
using Sum.Model.Options;
using Sum.Repository.ServiceExtension;
using Sum.Service.ServiceExtension;

namespace Sum.Api
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
            services.AddControllers();
            services.AddInjectionForSumObject(Configuration);
            services.AddInjectionForSumRepositories();
            services.AddInjectionForSumServices(Configuration);

            services.AddHealthChecks()
                .AddCheck("DB Health Check", () => DbHealthCheckProvider.Check(""))
                .AddCheck("Mq Health Check", ()=> MqHealthCheckProvider.Check(""))
                .AddCheck<SendgridHealthCheckProvider>("Sendgrid Health Check");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => { options.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("api/health");
                endpoints.MapControllers();
            });
        }
    }
}
