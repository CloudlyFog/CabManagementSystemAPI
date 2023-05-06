using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models;

[Table("Drivers")]
public class Driver
{
    [NotMapped]
    public static readonly Driver Default = new Driver()
    {
        Id = Guid.Empty,
        Name = "name",
        Experience = -1,
        IsBusy = true,
    };

    public Driver()
    {
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int Experience { get; set; }
    public bool IsBusy { get; set; }

    public Guid? CarId { get; set; }
    public Car? Car { get; set; }

    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }

    public static Driver SetDriver(Driver? driver, Car? car)
    {
        driver.Car = car;
        driver.CarId = car?.Id;

        return driver;
    }
}