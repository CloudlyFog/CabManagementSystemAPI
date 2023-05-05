

using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using CabManagementSystem.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CabManagementSystem.Services.Repositories;

public class CarRepository : OptionsUpdater, IRepository<Car>
{
    private readonly CabContext _cabContext;
    private bool _disposed;
    public CarRepository(ConfigurationOptions options)
    {
        UpdateOptions(options);
        _cabContext = new CabContext();
    }
    public IQueryable<Car> All =>
        _cabContext.Cars
        .Include(x => x.Driver)
        .Include(x => x.Order) ?? Enumerable.Empty<Car>().AsQueryable();

    public ExceptionModel Create(Car item)
    {
        throw new NotImplementedException();
    }

    public ExceptionModel Delete(Car item)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _cabContext.Dispose();
        _disposed = true;
    }

    public bool Exist(Expression<Func<Car, bool>> predicate)
    {
        return All.Any(predicate);
    }

    public bool FitsConditions(Car? item)
    {
        return item?.Driver is not null && Exist(x => x.Id == item.Id);
    }

    public Car Get(Expression<Func<Car, bool>> predicate)
    {
        return All.FirstOrDefault(predicate) ?? Car.Default;
    }

    public ExceptionModel Update(Car item)
    {
        throw new NotImplementedException();
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }
}
