using BankSystem7.Services.Configuration;
using CabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystem.Services.Configuration;

public class ModelConfiguration : ModelConfiguration<CabUser>
{
    public override void Invoke(ModelBuilder modelBuilder)
    {
        ConfigureUserRelationships(modelBuilder);
        ConfigureDriverRelationships(modelBuilder);
        base.Invoke(modelBuilder);
    }

    private void ConfigureDriverRelationships(ModelBuilder modelBuilder)
    {
        throw new NotImplementedException();
    }

    private void ConfigureUserRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CabUser>()
            .HasOne(x => x.user)
    }
}
