using BankSystem7.Models;
using BankSystem7.Services;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystem.Services.Abstract;

public abstract class OptionsUpdater : DbContextInitializer
{
    protected virtual void UpdateOptions(ConfigurationOptions options)
    {
        UpdateEnsureOperations(options.EnsureCreated, options.EnsureDeleted);
        UpdateOnModelCreating(options.Contexts);
    }
    protected void UpdateConnection(string connection)
    {
        ServiceConfiguration<CabUser, Card, BankAccount, Bank, Credit>.SetConnection(connection);
    }
    protected void UpdateDatabaseName(string databaseName)
    {
        ServiceConfiguration<CabUser, Card, BankAccount, Bank, Credit>.SetConnection(databaseName: databaseName);
    }
    protected void UpdateEnsureOperations(bool ensureCreated, bool ensureDeleted)
    {
        BankServicesOptions<CabUser, Card, BankAccount, Bank, Credit>.EnsureCreated = ensureCreated;
        BankServicesOptions<CabUser, Card, BankAccount, Bank, Credit>.EnsureDeleted= ensureDeleted;
    }
    protected void UpdateOnModelCreating(Dictionary<DbContext, ModelConfiguration>? contexts)
    {
        if (contexts is null)
            throw new ArgumentNullException(nameof(contexts));

        InitializeDbContexts(contexts);
    }
}
