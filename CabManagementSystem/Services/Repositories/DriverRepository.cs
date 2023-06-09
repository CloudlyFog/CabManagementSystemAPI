﻿using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CabManagementSystem.Services.Repositories;

public sealed class DriverRepository : OptionsUpdater<CabUser, Card, BankAccount, Bank, Credit>, IRepository<Driver>
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
        if (item?.Car is null || Exist(x => x.Id == item.Id))
            return ExceptionModel.OperationFailed;

        UpdateTracker(item, EntityState.Added);
        _cabContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    public ExceptionModel Delete(Driver item)
    {
        if (!FitsConditions(item))
            return ExceptionModel.OperationFailed;

        UpdateTracker(item, EntityState.Deleted);
        _cabContext.SaveChanges();
        return ExceptionModel.Ok;
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
        return item?.Car is not null && Exist(x => x.Id == item.Id);
    }

    public Driver Get(Expression<Func<Driver, bool>> predicate)
    {
        return All.FirstOrDefault(predicate) ?? Driver.Default;
    }

    public ExceptionModel Update(Driver item)
    {
        if (!FitsConditions(item))
            return ExceptionModel.OperationFailed;

        UpdateTracker(item, EntityState.Modified);
        _cabContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    private void UpdateTracker(Driver item, EntityState state)
    {
        _cabContext.UpdateTracker(item, state, delegate
        {
            _cabContext.AvoidChanges(new object[] { item.Car, item.Order });
        }, _cabContext);
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }
}