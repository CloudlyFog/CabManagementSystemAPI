using BankSystem7.Services.Interfaces;
using CabManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BankSystem7.Models;
using BankSystem7.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.AspNetCore.Http.HttpResults;
using CabManagementSystem.Extensions;

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
    public IActionResult All()
    {
        return Ok(_carRepository.All.Serialize());
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
