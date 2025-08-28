using Autofac;
using Infrastructure.IoC.Modules;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.IoC;

public class ContainerModule(IConfiguration configuration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<DbContextModule>();
        builder.RegisterModule<MediatrModule>();
        builder.RegisterModule<RepositoryModule>();
        builder.RegisterModule<ServiceModule>();
        builder.RegisterModule<NLogModule>();
        builder.RegisterModule<ValidationsModule>();
        builder.RegisterModule(new SettingsModule(configuration));
    }
}

