using CabManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CabManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        // GET: api/<DriverController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DriverController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DriverController>
        [HttpPost]
        public void Create(Driver driver)
        {
        }

        // PUT api/<DriverController>/5
        [HttpPut("{id}")]
        public void Update(Guid id, Driver driver)
        {
        }

        // DELETE api/<DriverController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
