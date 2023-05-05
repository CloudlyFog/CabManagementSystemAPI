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

    public void AvoidOrderChanges(Order order)
    {
        if (order is null)
            return;
        Entry(order.Car).State = EntityState.Unchanged;
        Entry(order.Driver).State = EntityState.Unchanged;
        Entry(order.User).State = EntityState.Unchanged;
    }

    public void AvoidChanges<T>(T item, Type[] types)
    {
        if (item is null || types is null || types.Length == 0)
            return;

        foreach (var prop in item.GetType().GetProperties())
        {
            if (types.Any(x => x == prop.GetType()))
                Entry(prop.GetValue(item)).State = EntityState.Unchanged;
        }
    }
}