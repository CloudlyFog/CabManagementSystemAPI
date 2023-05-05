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
        Entry(order.Car).State = EntityState.Unchanged;
        Entry(order.Driver).State = EntityState.Unchanged;
        Entry(order.User).State = EntityState.Unchanged;
    }

    public void AvoidDriverChanges(Driver driver)
    {
        if (driver is null)
            return;
        Entry(driver.Car).State = EntityState.Unchanged;
        Entry(driver.Order).State = EntityState.Unchanged;
    }

    public void AvoidCarChanges(Car car)
    {
        if (car is null)
            return;
        Entry(car.Driver).State = EntityState.Unchanged;
        Entry(car.Order).State = EntityState.Unchanged;
    }
}