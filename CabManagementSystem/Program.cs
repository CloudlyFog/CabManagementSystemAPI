using BankSystem7.Extensions;
using BankSystem7.Models;
using BankSystem7.Services;
using BankSystem7.Services.Configuration;
using CabManagementSystem.Data;
using CabManagementSystem.Models;
using CabManagementSystem.Services.Configuration;
using Microsoft.EntityFrameworkCore;

const string DatabaseName = "CabManagementSystem";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNationBankSystem<CabUser, Card, BankAccount, Bank, Credit>(o =>
{
    o.EnsureCreated = true;
    o.EnsureDeleted = true;
    o.DatabaseName = DatabaseName;
    o.OperationOptions = new OperationServiceOptions()
    {
        DatabaseName = DatabaseName,
    };
    o.Contexts = new Dictionary<DbContext, ModelConfiguration?>
    {
        { new CabContext(), new CabManagementSystemModelConfiguration() },
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
