using BankSystem7.Models;
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
    private bool _disposed;

    public OrderRepository(CabManagementOptions options)
    {
        UpdateOptions(options);
        _cabContext = new CabContext();
    }

    public IQueryable<Order> All =>
        _cabContext.Orders
        .Include(x => x.Car)
        .ThenInclude(x => x.Driver)
        .Include(x => x.Driver)
        .ThenInclude(x => x.Car)
        .Include(x => x.User)
        .ThenInclude(x => x.Orders) ?? Enumerable.Empty<Order>().AsQueryable();

    public ExceptionModel Create(Order item)
    {
        if (item?.Car is null || item?.Driver is null)
            return ExceptionModel.EntityIsNull;

        if (Exist(x => x.Id == item.Id))
            return ExceptionModel.EntityNotExist;

        if (!CheckOnOrderAbility(item, OrderOperationType.Order))
            return ExceptionModel.OperationFailed;

        UpdateTracker(item, EntityState.Added);
        _cabContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    public ExceptionModel Delete(Order item)
    {
        if (!FitsConditions(item))
            return ExceptionModel.EntityNotExist;

        SetOrderDetails(item, OrderOperationType.Reject);

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

    public bool Exist(Expression<Func<Order, bool>> predicate)
    {
        return All.Any(predicate);
    }

    public bool FitsConditions(Order? item)
    {
        return item?.Car is not null && item?.Driver is not null && Exist(x => x.Id == item.Id);
    }

    public Order Get(Expression<Func<Order, bool>> predicate)
    {
        return All.FirstOrDefault(predicate) ?? Order.Default;
    }

    public ExceptionModel Update(Order item)
    {
        if (!FitsConditions(item))
            return ExceptionModel.EntityNotExist;


        item.User.Orders.Remove(item);
        UpdateTracker(item, EntityState.Modified);

        _cabContext.SaveChanges();
        return ExceptionModel.Ok;
    }

    private void SetOrderDetails(Order? order, OrderOperationType operationType)
    {
        if (operationType == OrderOperationType.Order)
        {
            order.Car.IsBusy = true;
            order.Driver.IsBusy = true;
        }
        else
        {
            order.Car.IsBusy = false;
            order.Driver.IsBusy = false;
        }
    }

    private bool CheckOnOrderAbility(Order order, OrderOperationType operationType)
    {
        var ability = !order.Car.IsBusy && !order.Driver.IsBusy;
        SetOrderDetails(order, operationType);
        return ability;
    }

    protected override void UpdateOptions(ConfigurationOptions options)
    {
        UpdateDatabaseName(options.DatabaseName);
        base.UpdateOptions(options);
    }

    private void UpdateTracker(Order item, EntityState state)
    {
        _cabContext.UpdateTracker(item, state, delegate
        {
            _cabContext.AvoidOrderChanges(item);
        });
    }
}

internal enum OrderOperationType
{
    Order,
    Reject,
}