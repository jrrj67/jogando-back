using FluentValidation.AspNetCore;
using JogandoBack.API.Data.Contexts;
using JogandoBack.API.Data.DependecyInjections;
using JogandoBack.API.Data.Seeds;
using JogandoBack.API.Data.Services.Token;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting the web host.");

                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext());

                Log.Information("Adding services.");

                builder.Services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(@"/app/Temp/Keys"));

                builder.Services.AddControllers();

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                {
                    var tokenConfiguration = TokenService.GetTokenConfiguration(builder.Configuration);

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(TokenService.GetSecretKey(builder.Configuration)),
                        ValidIssuer = tokenConfiguration.Issuer,
                        ValidAudience = tokenConfiguration.Audience,
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };
                });

                builder.Services.AddHttpContextAccessor();

                builder.Services.AddDbContext<ApplicationDbContext>();

                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                builder.Services.AddFluentValidation();

                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
                          new string[] {}
                    }
                });
                });

                builder.Services.AddFluentValidationRulesToSwagger();

                PasswordHasherDI.RegisterDependencies(builder.Services);

                LoginDI.RegisterDependencies(builder.Services);

                TokenDI.RegisterDependencies(builder.Services);

                UsersDI.RegisterDependencies(builder.Services);

                RolesDI.RegisterDependencies(builder.Services);

                Log.Information("Building application.");

                var app = builder.Build();

                Log.Information("Adding usings in pipeline.");

                app.UseSerilogRequestLogging(configure =>
                {
                    configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
                });

                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

                Log.Information("Applying migrations.");

                Migrate(app);

                Log.Information("Applying seeds.");

                Seed.ApplySeed(app, builder.Configuration);

                Log.Information("Running application.");

                app.Run();
            }
            catch (Exception ex)
            {

                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static void Migrate(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.Migrate();
            }
        }
    }
}
