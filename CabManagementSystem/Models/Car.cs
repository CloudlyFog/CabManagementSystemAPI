using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models;

[Table("Cars")]
public class Car
{
    [NotMapped]
    public static readonly Car Default = new Car()
    {
        Id = Guid.Empty,
        Mileage = -1,
        IsBusy = true,
    };

    public Car()
    {
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public CarModel CarModel { get; set; }
    public int Mileage { get; set; }
    public bool IsBusy { get; set; }

    public Guid? DriverId { get; set; }
    public Driver? Driver { get; set; }

    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }

    public static Car SetCar(Car? car, Driver? driver)
    {
        car.Driver = driver;
        car.DriverId = driver?.Id;

        return car;
    }
}

public enum CarModel
{
    BMW,
    Mercedes,
    Lada,
    Chevrolet,
    Nissan,
}