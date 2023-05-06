using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Models;
using CabManagementSystem.Services.Repositories;

namespace CabManagementSystem.Extensions;

public static class CabManagementExtensions
{
    public static IServiceCollection AddCabManagementSystem(this IServiceCollection services, Action<ConfigurationOptions> options)
    {
        var resultOptions = new ConfigurationOptions();
        options?.Invoke(resultOptions);
        services.AddSingleton<IRepository<Car>>(new CarRepository(resultOptions));
        services.AddSingleton<IRepository<Driver>>(new DriverRepository(resultOptions));
        services.AddSingleton<IRepository<Order>>(new OrderRepository(resultOptions));
        return services;
    }
    public static IServiceCollection AddCabManagementSystem(this IServiceCollection services, ConfigurationOptions options)
    {
        services.AddSingleton<IRepository<Car>>(new CarRepository(options));
        services.AddSingleton<IRepository<Driver>>(new DriverRepository(options));
        services.AddSingleton<IRepository<Order>>(new OrderRepository(options));
        return services;
    }
}
