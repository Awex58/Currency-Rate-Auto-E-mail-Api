using Business.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using Redisdeneme.CacheService.Redis;
using WebApi.Core.DataAccess.EntityFrameworkDal.Abstract;
using WebApi.Core.DataAccess.EntityFrameworkDal.Concrete;
using WebApi.Core.DataAccess.TcmbAccess.Abstract;
using WebApi.Core.DataAccess.TcmbAccess.Concrete;
using WebApi.Core.MassTransit.Producer;
using WebApi.Core.Quartz;
using WebApi.Core.Utilites.Security.Encyption;
using WebApi.Core.Utilites.Security.Jwt;
using WebApi.Data.RedisServer;
using WebApi.Service.Abstract;
using WebApi.Service.Concrete;
using WebApi.Service.Redis;

namespace WebApi
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
            // burasý dependcy injection tanýmlama yeri
            services.AddSingleton<ICurrencyDal, EfCurrencyDal>();
            services.AddSingleton<ICurrencyService, CurrencyManager>();

            services.AddSingleton<ICurrencyRateDal, EfCurrenyRateDal>();
            services.AddSingleton<ICurrencyRateService, CurrencyRateManager>();

            services.AddSingleton<ITcmbAccess, TcmbAccess>();

            services.AddSingleton<RedisServer>();
            services.AddSingleton<ICacheService, RedisCacheService>();

            services.AddSingleton<IUserService, UserManager>();
            services.AddSingleton<IUserDal, EfUserDal>();

            services.AddSingleton<IAuthService, AuthManager>();
            services.AddSingleton<ITokenHelper, JwtHelper>();

            services.AddSingleton<Producer>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<QuartzJob>();


            services.AddSingleton(new JobSchedule(
                jobType: typeof(QuartzJob),
                cronExpression: "0/30 * * * * ?")); //  every min (* * * * *) every sec (* * * ? * *) every 30 sec 0/30 * * * * ?

            services.AddHostedService<QuartzHostedService>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Foreign Currency", Version = "v2" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // login için swaggera eklenir
                {
                    In = ParameterLocation.Header,
                    Description = "insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

            });

            services.AddStackExchangeRedisCache(action =>
            {
                action.Configuration = "localhost:6379";
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Foreign Currency v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
