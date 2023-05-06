using BankSystem7.Extensions;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Extensions;
using CabManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CabManagementSystem.Controllers;

[Route("api/cars")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IRepository<Car> _carRepository;

    public CarController(IRepository<Car> carRepository)
    {
        _carRepository = carRepository;
    }

    [HttpGet]
    public string All()
    {
        return _carRepository.All.Serialize();
    }

    [HttpGet("{id}")]
    public string Get(Guid id)
    {
        return _carRepository.Get(x => x.Id == id).Serialize();
    }

    [HttpPost]
    public void Create(Car car)
    {
        _carRepository.Create(car);
    }

    [HttpPut]
    public void Update(Guid id, Car car)
    {
        var getCar = _carRepository.Get(x => x.Id == id);
        _carRepository.Update(car.SetValuesTo(getCar));
    }

    [HttpDelete]
    public void Delete(Guid id)
    {
        var getCar = _carRepository.Get(x => x.Id == id);
        _carRepository.Delete(getCar);
    }
}