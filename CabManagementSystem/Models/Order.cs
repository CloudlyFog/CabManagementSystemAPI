using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models;

[Table("Orders")]
public class Order
{
    public static readonly Order Default = new()
    {
        AddressFrom = "default",
        AddressTo = "default",
        Price = -1,
        OrderDateTime = DateTime.Now,
    };
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string AddressFrom { get; set; }
    public string AddressTo { get; set; }
    public decimal Price { get; set; }
    public DateTime? OrderDateTime { get; set; }

    public Guid? UserId { get; set; }
    public CabUser? User { get; set; }

    public Guid? DriverId { get; set; }
    public Driver? Driver { get; set; }

    public Guid? CarId { get; set; }
    public Car? Car { get; set; }
}
