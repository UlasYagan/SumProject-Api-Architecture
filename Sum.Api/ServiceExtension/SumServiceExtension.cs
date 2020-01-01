using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sum.Domain.Entities;
using Sum.Model.Feature;
using Sum.Model.Options;

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

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Sum Project", Version = "v1" });

                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "sumAuth" }
                        }, new List<string>()
                    }
                };

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "sumAuth"
                });
                option.AddSecurityRequirement(security);
            });

            //services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.SaveToken = true;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });



        }
    }
}