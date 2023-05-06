using BankSystem7.Extensions;
using BankSystem7.Services.Interfaces;
using CabManagementSystem.Extensions;
using CabManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CabManagementSystem.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IRepository<Order> _orderRepository;
    public OrderController(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public string All()
    {
        return _orderRepository.All.Serialize();
    }

    [HttpGet("{id}")]
    public string Get(Guid id)
    {
        return _orderRepository.Get(x => x.Id == id).Serialize();
    }

    [HttpPost]
    public void Create(Order order)
    {
        _orderRepository.Create(order);
    }

    [HttpPut]
    public void Update(Guid id, Order order)
    {
        var updOrder = _orderRepository.Get(x => x.Id == id);
        _orderRepository.Update(order.SetValuesTo(updOrder));
    }

    [HttpDelete]
    public void Delete(Guid id)
    {
        var driver = _orderRepository.Get(x => x.Id == id);
        _orderRepository.Delete(driver);
    }
}
