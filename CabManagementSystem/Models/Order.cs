namespace CabManagementSystem.Models;

public class Order
{
    public Guid Id { get; set; }
    public string AddressFrom { get; set; }
    public string AddressTo { get; set; }
    public decimal Price { get; set; }
    public DateTime OrderDateTime { get; set; }
}
