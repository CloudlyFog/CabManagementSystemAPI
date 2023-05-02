using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystem.Models;

[Table("Drivers")]
public class Driver
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Experience { get; set; }
    public bool IsBusy { get; set; }

    public Guid? CarId { get; set; }
    public Car? Car { get; set; }

    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}
