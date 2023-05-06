

using BankSystem7.Models;
using BankSystem7.Services.Configuration;
using BankSystem7.Services.Repositories;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using CabManagementSystem.Services.Configuration;
using CabManagementSystem.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CabManagementSystemTest;

public class Tests
{
    private OrderRepository _orderRepository;
    private DriverRepository _driverRepository;
    private CarRepository _carRepository;
    private ServiceConfiguration<CabUser, Card, BankAccount, Bank, Credit> _serviceConfiguration =
        ServiceConfiguration<CabUser, Card, BankAccount, Bank, Credit>.CreateInstance(GetOptions());
    private CabContext _cabContext;
    [SetUp]
    public void Setup()
    {
        _orderRepository = new(GetOptions());
        _driverRepository = new(GetOptions());
        _carRepository = new(GetOptions());
        _cabContext = new CabContext();
    }

    [Test]
    public void SetupDatabase()
    {
        var driver = GetDriver();
        var car = GetCar();
        var order = GetOrder();
        var user = GetUser();
        _driverRepository.Create(driver);
        _carRepository.Create(car);
        _serviceConfiguration.UserRepository.Create(user);
        _orderRepository.Create(order);

        SetOrderDetails(order, driver, car);

        var updDriver = _driverRepository.Get(x => x.Id == driver.Id);
        _driverRepository.Update(updDriver);

        var updCar = _carRepository.Get(x => x.Id == car.Id);
        _carRepository.Update(updCar);

        Assert.Pass();
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
        var user = GetUser();
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
            ID = new Guid("4AB6414C-AD4C-4094-BC91-869E0CF65429"),
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

    private void SetOrderDetails(Order order, Driver driver, Car car)
    {
        driver.Order = order;
        driver.OrderId = order.Id;

        car.Order = order;
        car.OrderId = order.Id;
    }

    private static ConfigurationOptions GetOptions()
    {
        return new ConfigurationOptions()
        {
            DatabaseName = "CabManagementSystem",
            EnsureCreated = true,
            EnsureDeleted = true,
            Contexts = new Dictionary<DbContext, ModelConfiguration?>
            {
                { new CabContext(), new CabManagementSystemModelConfiguration(true) }
            },
        };
    }
}