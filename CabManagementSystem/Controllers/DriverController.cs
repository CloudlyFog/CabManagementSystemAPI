using BankSystem7.Extensions;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Extensions;
using CabManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CabManagementSystem.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IRepository<Driver> _driverRepository;

        public DriverController(IRepository<Driver> driverRepository)
        {
            _driverRepository = driverRepository;
        }

        [HttpGet]
        public string All()
        {
            return _driverRepository.All.Serialize();
        }

        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            return _driverRepository.Get(x => x.Id == id).Serialize();
        }

        [HttpPost]
        public void Create(Driver driver)
        {
            _driverRepository.Create(driver);
        }

        [HttpPut("{id}")]
        public void Update(Guid id, Driver driver)
        {
            var updDriver = _driverRepository.Get(x => x.Id == id);
            _driverRepository.Update(driver.SetValuesTo(updDriver));
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var driver = _driverRepository.Get(x => x.Id == id);
            _driverRepository.Delete(driver);
        }
    }
}