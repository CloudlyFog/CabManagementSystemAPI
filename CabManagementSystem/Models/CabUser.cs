using BankSystem7.Models;

namespace CabManagementSystem.Models;

public class CabUser : User
{
    public Guid? OrderId { get; set; }
    public List<Order>? Orders { get; set; } = new();
}
