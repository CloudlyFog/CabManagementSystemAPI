namespace CabManagementSystem.Models;

public class Car
{
    public Guid Id { get; set; }
    public CarModel CarModel { get; set; }
    public int Mileage { get; set; }
}

public enum CarModel
{
    BMW,
    Mercedes,
    Lada,
    Chevrolet,
    Nissan,
}   
