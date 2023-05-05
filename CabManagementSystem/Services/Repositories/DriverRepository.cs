using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using CabManagementSystem.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CabManagementSystem.Services.Repositories;

public sealed class DriverRepository : OptionsUpdater, IRepository<Driver>
{
    private readonly CabContext _cabContext;
    private bool _disposed;

    public DriverRepository(ConfigurationOptions options)
    {
        UpdateOptions(options);
        _cabContext = new CabContext();
    }
    public IQueryable<Driver> All =>
        _cabContext.Drivers
        .Include(x => x.Car)
        .Include(x => x.Order) ?? Enumerable.Empty<Driver>().AsQueryable();

    public ExceptionModel Create(Driver item)
    {
        throw new NotImplementedException();
    }

    public ExceptionModel Delete(Driver item)
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

    public bool Exist(Expression<Func<Driver, bool>> predicate)
    {
        return All.Any(predicate);
    }

    public bool FitsConditions(Driver? item)
    {
        return item?.Car is not null && item?.Order is not null && Exist(x => x.Id == item.Id);
    }

    public Driver Get(Expression<Func<Driver, bool>> predicate)
    {
        return All.FirstOrDefault(predicate) ?? Driver.Default;
    }

    public ExceptionModel Update(Driver item)
    {
        throw new NotImplementedException();
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }
}
