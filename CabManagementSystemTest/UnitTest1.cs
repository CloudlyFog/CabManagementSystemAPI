

namespace CabManagementSystemTest
{
    public class Tests
    {
        // private readonly OrderRepository _orderRepository;
        [SetUp]
        public void Setup()
        {
            //_orderRepository = new();
        }

        [Test]
        public void Test1()
        {
            var order = GetOrder();
            //_orderRepository.Create(order);
            Assert.Pass();
        }

        private object GetOrder()
        {
            return 1;
        }
    }
}