﻿using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using CabManagementSystem.Services.Abstract;
using CabManagementSystem.Services.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CabManagementSystem.Services.Repositories;

public sealed class OrderRepository : OptionsUpdater, IRepository<Order>
{
    private readonly CabContext _cabContext;

    public OrderRepository(CabManagementOptions options)
    {
        UpdateOptions(options);
        _cabContext = new CabContext();
    }

    public IQueryable<Order> All =>
        _cabContext.Orders
        .Include(x => x.User)
        .ThenInclude(x => x.Orders)
        .Include(x => x.Car)
        .ThenInclude(x => x.Driver)
        .Include(x => x.Driver)
        .ThenInclude(x => x.Car)
        .AsNoTracking() ?? Enumerable.Empty<Order>().AsQueryable();

    public ExceptionModel Create(Order item)
    {
        if (item?.Car is null || item?.Driver is null)
            return ExceptionModel.EntityIsNull;

        if (Exist(x => x.Id == item.Id))
            return ExceptionModel.EntityNotExist;

        SetOrderDetails(item);

        _cabContext.ChangeTracker.Clear();
        _cabContext.Orders.Add(item);
        _cabContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    public ExceptionModel Delete(Order item)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool Exist(Expression<Func<Order, bool>> predicate)
    {
        return All.Any(predicate);
    }

    public bool FitsConditions(Order? item)
    {
        return item?.Car is not null || item?.Driver is not null || !Exist(x => x.Id == item.Id);
    }

    public Order Get(Expression<Func<Order, bool>> predicate)
    {
        return All.FirstOrDefault(predicate) ?? Order.Default;
    }

    public ExceptionModel Update(Order item)
    {
        throw new NotImplementedException();
    }

    private void SetOrderDetails(Order? order)
    {
        order.Car.IsBusy = true;
        order.Driver.IsBusy = true;
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }
}