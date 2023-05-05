using BankSystem7.AppContext;
using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystem.Data;

public class CabContext : GenericDbContext<CabUser, Card, BankAccount, Bank, Credit>
{
    public CabContext()
    {
    }

    public CabContext(ModelConfiguration? modelConfiguration) : base(modelConfiguration)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    public void AvoidOrderChanges(Order order)
    {
        if (order is null)
            return;
        if (order?.Car is not null)
            Entry(order?.Car).State = EntityState.Unchanged;
        if (order?.Driver is not null)
            Entry(order?.Driver).State = EntityState.Unchanged;
        if (order?.User is not null)
            Entry(order?.User).State = EntityState.Unchanged;
    }

    public void AvoidDriverChanges(Driver driver)
    {
        if (driver is null)
            return;

        if (driver?.Car is not null)
            Entry(driver?.Car).State = EntityState.Unchanged;
        if (driver?.Order is not null)
            Entry(driver?.Order).State = EntityState.Unchanged;
    }

    public void AvoidCarChanges(Car car)
    {
        if (car is null)
            return;

        if (car?.Driver is not null)
            Entry(car?.Driver).State = EntityState.Unchanged;
        if (car?.Order is not null)
            Entry(car?.Order).State = EntityState.Unchanged;
    }

    public void UpdateTracker<T>(T item, EntityState state, Action action)
    {
        ChangeTracker.Clear();
        Entry(item).State = state;
        action.Invoke();
    }
}