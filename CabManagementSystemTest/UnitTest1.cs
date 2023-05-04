

using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Repositories;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using CabManagementSystem.Services.Configuration;
using CabManagementSystem.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemTest;

public class Tests
{
    private OrderRepository _orderRepository;
    [SetUp]
    public void Setup()
    {
        _orderRepository = new(new CabManagementOptions()
        {
            DatabaseName = "Test2",
            EnsureCreated = true,
            EnsureDeleted = true,
            Contexts = new Dictionary<DbContext, ModelConfiguration?>
            {
                { new CabContext(), new CabManagementSystemModelConfiguration(true) }
            },
            InitializeAccess = true,
        });
    }

    [Test]
    public void Test1()
    {
        var order = GetOrderNew();
        _orderRepository.Create(order);
        var newOrder = _orderRepository.Get(x => x.Id == order.Id);
        Assert.That(newOrder, Is.EqualTo(new Order()));
        Assert.Pass();
    }
    private Order GetOrderNew()
    {
        var order = new Order()
        {
            AddressFrom = "addrFrom",
            AddressTo = "addrTo",
            Price = 100,
        };
        var car = new Car()
        {
            CarModel = CarModel.Mercedes,
            Mileage = 1000,
            IsBusy = true,
        };
        var driver = new Driver()
        {
            Name = "Alex",
            Experience = 10,
            IsBusy = true,
        };

        var user = new CabUser(User.Default);
        return Order.SetOrder(order, driver, car, user);
    }
    private Order GetOrder()
    {
        var user = GetUser();
        var order = new Order(user)
        {
            AddressFrom = "addrFrom",
            AddressTo = "addrTo",
            Price = 100,
        };
        
        var car = new Car(order)
        {
            CarModel = CarModel.Mercedes,
            Mileage = 1000,
            IsBusy = true,
        };
        order.Driver = new Driver(order)
        {
            Name = "Alex",
            Experience = 10,
            IsBusy = true,
            Car = car,
            CarId = car.Id,
        };
        order.Car = car;
        order.Car.Driver = order.Driver;
        order.Driver.Car = order.Car;

        order = new Order(order.Driver, order.Car, order.User)
        {
            AddressFrom = "addrFrom",
            AddressTo = "addrTo",
            Price = 100,
        };
        
        
        return order;
    }

    private CabUser GetUser()
    {
        var bank = new Bank
        {
            ID = new Guid("376BAC1B-0CC2-4CCE-B10A-D21570CA3736"),
            BankName = "Tinkoff",
        };

        var bank2 = new Bank()
        {
            ID = new Guid("5A3DBB2C-33D9-45E7-B108-C99928E488B7"),
            BankName = "AlphaBank",
        };

        var user = new CabUser()
        {
            Name = "alex",
            Email = "alex@gmail.com",
            Password = "test",
            PhoneNumber = "1613750023",
            Age = 17
        };
        var bankAccount = new BankAccount(user, bank)
        {
            BankAccountAmount = 10_000,
        };

        user.Card = new Card(user.Age, user: user, bankAccount: bankAccount);

        return user;
    }
}