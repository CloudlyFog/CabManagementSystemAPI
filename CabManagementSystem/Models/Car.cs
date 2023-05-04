using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models;

[Table("Cars")]
public class Car
{
    public Car()
    {
        
    }
    public Car(Order order)
    {
        Order = order;
        OrderId = order.Id;

        Driver = order.Driver;
        DriverId = order.DriverId;
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public CarModel CarModel { get; set; }
    public int Mileage { get; set; }
    public bool IsBusy { get; set; }

    public Guid? DriverId { get; set; }
    public Driver? Driver { get; set; }

    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}

public enum CarModel
{
    BMW,
    Mercedes,
    Lada,
    Chevrolet,
    Nissan,
}   
