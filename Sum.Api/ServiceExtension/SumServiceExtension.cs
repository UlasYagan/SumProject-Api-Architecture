using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sum.Domain.Entities;
using Sum.Model.Feature;

namespace Sum.Api.ServiceExtension
{
    public static class SumServiceExtension
    {
        public static void AddInjectionForSumObject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(configuration.GetConnectionString("Connection")));

            //Inject AppSettings
            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<FileUpload>(configuration.GetSection(nameof(FileUpload)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSwaggerGen(option => { option.SwaggerDoc("v1", new OpenApiInfo { Title = "Sum Project", Version = "v1" }); });



            //services.AddAuthentication("ApplicationCookie").AddCookie("ApplicationCookie", options =>
            //{
            //    options.LoginPath = "/Auth/Login";
            //    options.LogoutPath = "/Auth/Logout";
            //    options.ExpireTimeSpan = TimeSpan.FromDays(5);
            //    options.SlidingExpiration = true;
            //    options.ReturnUrlParameter = "ReturnUrl";
            //});
            //services.AddAuthorization();
        }
    }
}