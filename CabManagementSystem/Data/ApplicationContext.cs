using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CabManagementSystem.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
        
    }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseSqlServer(ServiceConfiguration<CabUser, Card, BankAccount, Bank, Credit>.Connection);
    }
}
