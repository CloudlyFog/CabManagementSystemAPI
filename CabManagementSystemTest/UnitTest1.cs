

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
    private DriverRepository _driverRepository;
    private CarRepository _carRepository;
    private CabContext _cabContext;
    [SetUp]
    public void Setup()
    {
        var options = new ConfigurationOptions()
        {
            DatabaseName = "Test2",
            EnsureCreated = true,
            EnsureDeleted = true,
            Contexts = new Dictionary<DbContext, ModelConfiguration?>
            {
                { new CabContext(), new CabManagementSystemModelConfiguration(true) }
            },
        };
        _orderRepository = new(options);
        _driverRepository = new(options);
        _carRepository = new(options);
        _cabContext = new CabContext();
    }

    [Test]
    public void TestOrderRepository()
    {
        var order = GetOrder();
        _orderRepository.Create(order);
        var newOrder = _orderRepository.Get(x => x.Id == order.Id);
        newOrder.AddressFrom = "ksdfjsdklfsd";
        _orderRepository.Update(newOrder);
        _orderRepository.Delete(order);
        Assert.That(newOrder, Is.EqualTo(new Order()));
        Assert.Pass();
    }

    [Test]
    public void TestAvoidChanges()
    {
        var order = GetOrder();
        Assert.Pass();
    }

    [Test]
    public void TestDriverRepository()
    {
        var driver = GetDriver();
        _driverRepository.Create(driver);
        var newDriver = _driverRepository.Get(x => x.Id == driver.Id);
        newDriver.Name = "nsfkldjfsdlf";
        _driverRepository.Update(newDriver);
        _carRepository.Create(GetCar());
        _driverRepository.Delete(newDriver);
        Assert.Pass();
    }

    [Test]
    public void TestCarRepository()
    {
        var car = GetCar();
        var driver = GetDriver();
        _driverRepository.Create(driver);
        _carRepository.Create(car);
        _driverRepository.Delete(driver);
        _driverRepository.Create(driver);

        var newCar = _carRepository.Get(x => x.Id == car.Id);
        newCar.Mileage = 10000;
        _carRepository.Update(newCar);
        _carRepository.Delete(newCar);
        Assert.Pass();
    }

    private Order GetOrder()
    {
        var user = new CabUser(User.Default);
        return Order.SetOrder(GetDefaultOrder(), GetDefaultDriver(), GetDefaultCar(), user);
    }

    private Driver GetDriver()
    {
        return Driver.SetDriver(GetDefaultDriver(), GetDefaultCar());
    }

    private Car GetCar()
    {
        return Car.SetCar(GetDefaultCar(), GetDefaultDriver());
    }

    private Car GetDefaultCar()
    {
        return new Car()
        {
            Id = new Guid("B4AFE754-085D-4CB4-B394-B914ACE39BF4"),
            CarModel = CarModel.Mercedes,
            Mileage = 1000,
        };
    }

    private Driver GetDefaultDriver()
    {
        return new Driver()
        {
            Id = new Guid("1A37EDEF-3BA8-4EB0-9ABD-64FA92E875BC"),
            Name = "Alex",
            Experience = 10,
        };
    }

    private Order GetDefaultOrder()
    {
        return new Order()
        {
            Id = new Guid("3831AA12-0DAE-4B31-9E41-709B54624FE4"),
            AddressFrom = "addrFrom",
            AddressTo = "addrTo",
            Price = 100,
        };
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