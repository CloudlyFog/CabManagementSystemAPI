using BankSystem7.Services.Configuration;

namespace CabManagementSystem.Services.Configuration;

public class CabManagementOptions : ConfigurationOptions
{
    public bool InitializeAccess { get; set; }
}