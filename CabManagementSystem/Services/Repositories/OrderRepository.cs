using BankSystem7.Models;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CabManagementSystem.Services.Repositories;

public sealed class OrderRepository : IRepository<Order>
{
    private readonly ApplicationContext _applicationContext;
    public OrderRepository()
    {
        _applicationContext = new ApplicationContext();
    }
    public IQueryable<Order> All =>
        _applicationContext.Orders
        .Include(x => x.Car)
        .Include(x => x.Driver)
        .AsNoTracking() ?? Enumerable.Empty<Order>().AsQueryable();

    public ExceptionModel Create(Order item)
    {
        if (item?.Car is null || item?.Driver is null)
            return ExceptionModel.EntityIsNull;

        if (!Exist(x => x.Id == item.Id))
            return ExceptionModel.EntityNotExist;

        _applicationContext.ChangeTracker.Clear();
        _applicationContext.Orders.Add(item);
        _applicationContext.SaveChanges();
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
}
