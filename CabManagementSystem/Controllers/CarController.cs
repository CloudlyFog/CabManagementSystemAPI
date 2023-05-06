using BankSystem7.Services.Interfaces;
using CabManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BankSystem7.Models;
using BankSystem7.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public IActionResult GetAll()
    {
        var json = JsonConvert.SerializeObject(_carRepository.All, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        return Ok(json);
    }

    [HttpGet("{id}")]
    public IActionResult GetCar(Guid id)
    {
        var car = _carRepository.Get(x => x.Id == id);
        var json = JsonConvert.SerializeObject(car, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        return Ok(json);
    }

    [HttpPost]
    public IActionResult CreateCar(Car car)
    {
        var create = _carRepository.Create(car);
        if (create is not ExceptionModel.Ok or ExceptionModel.Successfully)
            return BadRequest();
        return Ok(create);
    }

    [HttpPut]
    public IActionResult UpdateCar(Guid id, Car car)
    {
        var getCar = _carRepository.Get(x => x.Id == id);
        var update = _carRepository.Update(car.SetValuesTo(getCar));
        if (update is not ExceptionModel.Ok or ExceptionModel.Successfully)
            return BadRequest();
        return Ok(update);
    }

    [HttpDelete]
    public IActionResult DeleteCar(Guid id)
    {
        var getCar = _carRepository.Get(x => x.Id == id);
        var delete = _carRepository.Delete(getCar);
        if (delete is not ExceptionModel.Ok or ExceptionModel.Successfully)
            return BadRequest();
        return Ok(delete);
    }
}
