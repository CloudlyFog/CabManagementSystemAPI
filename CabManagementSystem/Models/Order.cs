namespace CabManagementSystem.Models;

public class Order
{
    public Guid? Id { get; set; }
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
