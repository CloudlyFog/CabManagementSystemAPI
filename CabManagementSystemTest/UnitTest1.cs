

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
            EnsureCreated = false,
            EnsureDeleted = false,
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
        newOrder.AddressFrom = "ksdfjsdklfsd";
        _orderRepository.Update(newOrder);
        _orderRepository.Delete(order);
        Assert.That(newOrder, Is.EqualTo(new Order()));
        Assert.Pass();
    }
    private Order GetOrderNew()
    {
        var order = new Order()
        {
            Id = new Guid("3831AA12-0DAE-4B31-9E41-709B54624FE4"),
            AddressFrom = "addrFrom",
            AddressTo = "addrTo",
            Price = 100,
        };
        var car = new Car()
        {
            Id = new Guid("B4AFE754-085D-4CB4-B394-B914ACE39BF4"),
            CarModel = CarModel.Mercedes,
            Mileage = 1000,
        };
        var driver = new Driver()
        {
            Id = new Guid("1A37EDEF-3BA8-4EB0-9ABD-64FA92E875BC"),
            Name = "Alex",
            Experience = 10,
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