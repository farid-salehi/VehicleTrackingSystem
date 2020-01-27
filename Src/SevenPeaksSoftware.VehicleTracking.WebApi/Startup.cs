using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SevenPeaksSoftware.VehicleTracking.Application.Settings;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Infrastructure;
using SevenPeaksSoftware.VehicleTracking.Ioc;
using Swashbuckle.AspNetCore.Swagger;

namespace SevenPeaksSoftware.VehicleTracking.WebApi
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
            services.AddServices();

            services.AddDbContext<VehicleTrackingDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<VehicleTrackingSettings>(options => Configuration.GetSection("VehicleTrackingSettings").Bind(options));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = Configuration.GetSection("VehicleTrackingSettings")
                        .GetSection("IdentitySettings").GetValue<string>("Audience"),

                    ValidIssuer = Configuration.GetSection("VehicleTrackingSettings")
                        .GetSection("IdentitySettings").GetValue<string>("Issuer"),

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration
                        .GetSection("VehicleTrackingSettings").GetSection("IdentitySettings")
                        .GetValue<string>("SecretKey")))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "7Software.VehicleTracking", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer" +
                                  " scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
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
         
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbInitializer = serviceScope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitializer.Migrate();
                dbInitializer.Seed();
            }
            app.UseMvc();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
         
        }
    }
}
