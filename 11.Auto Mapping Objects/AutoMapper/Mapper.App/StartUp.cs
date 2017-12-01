using AutoMapper;
using Mapper.Data;
using Mapper.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mapper.App
{
    class StartUp
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<EmployeeContext>(options=>
            options.UseSqlServer(ServerConfig.ConnectionStr));

            serviceCollection.AddTransient<EmployeeService>();
            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
