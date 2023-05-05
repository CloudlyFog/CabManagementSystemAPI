using BankSystem7.Services.Configuration;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystem.Services.Configuration;

public class CabManagementSystemModelConfiguration : ModelConfiguration
{
    public CabManagementSystemModelConfiguration()
    {
        
    }

    public CabManagementSystemModelConfiguration(bool initializeAccess) : base(initializeAccess)
    {
    }
    public override void Invoke(ModelBuilder modelBuilder)
    {
        ConfigureDriverRelationships(modelBuilder);
        ConfigureOrderRelationships(modelBuilder);
        base.Invoke(modelBuilder);
    }

    private void ConfigureOrderRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(order => order.Driver)
            .WithOne(driver => driver.Order)
            .HasForeignKey<Driver>(driver => driver.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Car)
            .WithOne(car => car.Order)
            .HasForeignKey<Car>(order => order.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.User)
            .WithMany(user => user.Orders);
    }

    private void ConfigureDriverRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>()
            .HasOne(driver => driver.Car)
            .WithOne(car => car.Driver)
            .HasForeignKey<Car>(car => car.DriverId);
    }
}
