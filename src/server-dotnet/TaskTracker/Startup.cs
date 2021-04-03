using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using TaskTracker.Misc;
using TaskTracker.Misc.Auth;


namespace TaskTracker
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
            Initializer.RegisterDependencies(services);

            Initializer.RegisterErrors();

            Initializer.RegisterMappings();

            services.AddSignalR().AddJsonProtocol(x =>
            {
                x.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddControllers(x =>
            {
                x.Filters.Add(typeof(LoggingFilter));
                x.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            }).AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddCors(x => x.AddPolicy("DefaultPolicy", builder =>
            {
                builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
            }));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"])),
                    ClockSkew = TimeSpan.Zero,
                };

                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        StringValues token = StringValues.Empty;

                        if (!context.Request.Headers.TryGetValue("x-access-token", out token))
                        {
                            context.NoResult();
                            return Task.CompletedTask;
                        }

                        context.Token = token.ToString();

                        // If no token found, no further work possible
                        if (string.IsNullOrEmpty(context.Token))
                        {
                            context.NoResult();
                            return Task.CompletedTask;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(x =>
            {
                x.AddPolicy(Policy.Admin, Policy.AdminPolicy());
                x.AddPolicy(Policy.User, Policy.UserPolicy());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Initializer.ConstructDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors("DefaultPolicy");
            }

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                            .RequireAuthorization();

                endpoints.MapHub<SignalRHub>("/api/v1/SignalR");
            });
        }

    }
}
