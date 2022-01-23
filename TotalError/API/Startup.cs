using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using API.Scheduler;

using Data;

using Infrastructure.DtoModels;
using Infrastructure.Interfaces;
using Infrastructure.Mapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Quartz;

using Services.Implementations;

namespace API
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
            services.AddRouting();


            // My Services --->
            services.AddScoped<IMainAPIService, MainAPIService>();
            services.AddScoped<ApplicationDbContext>();
            services.AddAutoMapper(typeof(MapProfile));
            services.AddScoped<IIdentityUser, IdentityUserService>();
            // <--- My Services


            // JWT --->
            services.Configure<TokenModel>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenModel>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                        ValidIssuer = token.Issuer,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            // <--- JWT


            // Quartz --->
            services.AddQuartz(q => 
            {
                q.SchedulerId = "Scheduler-Core";
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseInMemoryStore();
                q.ScheduleJob<SchedulerReader>(trigger => trigger
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.Now.AddSeconds(5)))
                    //.WithDailyTimeIntervalSchedule(x => x.WithIntervalInHours(12))
                    .WithDescription("Scheduler was triggered."));
                q.AddJob<SchedulerReader>(job => job.StoreDurably().WithDescription("Run job."));
               
            });
            services.AddQuartzHostedService(options => 
            {
                options.WaitForJobsToComplete = true;
            });
            // <--- Quartz


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.Use((req, res) =>
            {
                var headers = req.Request.Headers.ToList();
                return res.Invoke();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
