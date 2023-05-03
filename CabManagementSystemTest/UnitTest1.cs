

using CabManagementSystem.Models;
using CabManagementSystem.Services.Repositories;

namespace CabManagementSystemTest
{
    public class Tests
    {
        private OrderRepository _orderRepository;
        [SetUp]
        public void Setup()
        {
            _orderRepository = new();
        }

        [Test]
        public void Test1()
        {
            var order = GetOrder();
            _orderRepository.Create(order);
            Assert.Pass();
        }

        private Order GetOrder()
        {
            var car = new Car();

            var order = new Order();
            return order;
        }
    }
}