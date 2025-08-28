using API_Template.Middlewares;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints.Extensions;
using Infrastructure.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Shared.Extensions;
using Shared.Settings;
using System.Text;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

#pragma warning disable S2139 // Exceptions should be either logged or rethrown but not both
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddAuthorization();

    IWebHostEnvironment environment = builder.Environment;
    ConfigurationManager configuration = builder.Configuration;

    configuration.SetBasePath(environment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .AddEnvironmentVariables();

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
    builder.Host.UseNLog();

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new ContainerModule(configuration));
    });

    builder.Host.UseNLog();

    builder.Services.AddCors();

    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Token:IssuerKey"],
        ValidAudience = configuration["Token:AudienceKey"],
        IssuerSigningKeys = configuration["Token:SecretKeys"]?.Split(",", StringSplitOptions.TrimEntries).Select(s => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s)))
    });


    builder.Services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
        options.SerializerOptions.PropertyNamingPolicy = null;
        options.SerializerOptions.DictionaryKeyPolicy = null;
        options.SerializerOptions.WriteIndented = true;
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Api",
            Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Scheme = "bearer",
            Description = "Please insert JWT token into field"
        });
        c.IncludeXmlComments(AppContext.BaseDirectory + "api.xml");
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
        }
        });
    });

    var app = builder.Build();

    app.UseCors(c =>
    {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.AllowAnyOrigin();
    });

    app.ConfigureExceptionHandler();

    var responsetimesettings = app.Services.GetService<ResponseTimeSettings>();
    if (responsetimesettings?.Enabled ?? false)
    {
        app.ConfigureResponseTime();
    }

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api "));

    app.UseHsts();

    app.UseAuthorization();
    app.UseRouting();
    app.UseAuthentication();

    app.ConfigureBuffer();
    app.RequestLog();


    app.UseMinimalEndpoints(c => c.ProjectName = "EndpointsController");

    logger.Debug("Starting program");
    await app.RunAsync();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
#pragma warning restore S2139 // Exceptions should be either logged or rethrown but not both
