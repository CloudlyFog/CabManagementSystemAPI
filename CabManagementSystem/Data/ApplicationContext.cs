using BankSystem7.AppContext;
using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystem.Data;

public class ApplicationContext : GenericDbContext<CabUser, Card, BankAccount, Bank, Credit>
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(ModelConfiguration modelConfiguration) : base(modelConfiguration)
    {
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Car> Cars { get; set; }
}
