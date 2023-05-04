using BankSystem7.Models;
namespace CabManagementSystem.Models;

public class CabUser : User
{
    public CabUser()
    {
        
    }
    public CabUser(User user)
    {
        foreach (var prop in user.GetType().GetProperties())
            user.GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(user));
    }
    public List<Order>? Orders { get; set; } = new();
}