using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models;

[Table("Orders")]
public class Order
{
    [NotMapped]
    public static readonly Order Default = new()
    {
        AddressFrom = "default",
        AddressTo = "default",
        Price = -1,
        OrderDateTime = DateTime.Now,
    };

    public Order()
    {
        
    }
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string AddressFrom { get; set; }
    public string AddressTo { get; set; }
    public decimal Price { get; set; }
    public DateTime? OrderDateTime { get; set; } = DateTime.Now;

    public Guid? UserId { get; set; }
    public CabUser? User { get; set; }

    public Guid? DriverId { get; set; }
    public Driver? Driver { get; set; }

    public Guid? CarId { get; set; }
    public Car? Car { get; set; }

    public static Order SetOrder(Order order, Driver driver, Car car, CabUser user)
    {
        order.Car = car;
        order.CarId = car.Id;

        order.Driver = driver;
        order.DriverId = driver.Id;

        order.User = user;
        order.UserId = user.ID;
        order.User.Orders.Add(order);


        order.Car.Driver = order.Driver;
        order.Car.DriverId = order.DriverId;

        order.Driver.Car = order.Car;
        order.Driver.CarId = order.CarId;

        order.Car.Order = order;
        order.Driver.Order = order;

        return order;
    }
}
