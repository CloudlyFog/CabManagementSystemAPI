using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models;

[Table("Drivers")]
public class Driver
{
    public Driver()
    {
        
    }
    public Driver(Order order)
    {
        Order = order;
        OrderId = order.Id;

        Car = order.Car;
        CarId = order.CarId;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int Experience { get; set; }
    public bool IsBusy { get; set; }

    public Guid? CarId { get; set; }
    public Car? Car { get; set; }

    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}
