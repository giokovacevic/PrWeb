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
using PrWebBackend.Repositories.Implementations;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Implementations;
using PrWebBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrWebBackend
{
    public class Startup
    {
        private readonly string _cors = "AllowViteReactApp";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    RoleClaimType = ClaimTypes.Role
                };
            });


            services.AddScoped<IRoleRepository>(serviceProvider => new RoleRepository(connectionString));
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUserRepository>(serviceProvider => new UserRepository(connectionString));
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IQuizRepository>(serviceProvider => new QuizRepository(connectionString));
            services.AddScoped<IQuizService, QuizService>();

            string allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => 
                {
                    builder.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrWebBackend", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrWebBackend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_cors);

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
