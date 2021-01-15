using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

using Chat.Services.Interfaces;
using Chat.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Chat.Data.Authentication;
using Chat.Middlewares;

namespace Chat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContextPool<MSSQLContext>(options =>
                                        options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnectionString")));
            services.AddIdentity<User,UserRole>()
                .AddEntityFrameworkStores<MSSQLContext>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<IDataContext, MSSQLContext>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
