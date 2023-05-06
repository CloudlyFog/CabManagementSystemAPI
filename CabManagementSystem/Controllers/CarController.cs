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
    public IActionResult Get(Guid id)
    {
        var car = _carRepository.Get(x => x.Id == id);
        return Ok(car.Serialize());
    }

    [HttpPost]
    public IActionResult Create(Car car)
    {
        var create = _carRepository.Create(car);
        if (create is not ExceptionModel.Ok or ExceptionModel.Successfully)
            return BadRequest();
        return Ok(create);
    }

    [HttpPut]
    public IActionResult Update(Guid id, Car car)
    {
        var getCar = _carRepository.Get(x => x.Id == id);
        var update = _carRepository.Update(car.SetValuesTo(getCar));
        if (update is not ExceptionModel.Ok or ExceptionModel.Successfully)
            return BadRequest();
        return Ok(update);
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        var getCar = _carRepository.Get(x => x.Id == id);
        var delete = _carRepository.Delete(getCar);
        if (delete is not ExceptionModel.Ok or ExceptionModel.Successfully)
            return BadRequest();
        return Ok(delete);
    }
}
