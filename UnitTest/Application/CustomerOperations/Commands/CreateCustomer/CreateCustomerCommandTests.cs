using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStore.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStore.DBOperations;
using MovieStore.Entities;
using TestSetup;
using Xunit;

namespace Application.CustomerOperations.Commands.CreateCustomer
{
  public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly MovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCustomerCommandTests(CommonTestFixture testFixture)
    {
      _dbContext = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistingCustomerEmailIsGiven_Handle_ThrowsInvalidOperationException()
    {
      Customer customer = new Customer()
      {
        Email = "existing@example.com",
        Password = "existing123",
        FirstName = "existingfn",
        LastName = "existingln",

      };
      _dbContext.Customers.Add(customer);
      _dbContext.SaveChanges();

      // arrange
      CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
      command.Model = new CreateCustomerModel()
      {
        Email = "existing@example.com",
        Password = "existing123",
        FirstName = "existingfn",
        LastName = "existingln",
      };

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Kullanıcı zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
    {
      // arrange
      CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
      var model = new CreateCustomerModel()
      {
        Email = "new@example.com",
        Password = "new123123",
        FirstName = "newcustomer",
        LastName = "newcustomerln",
      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      var customer = _dbContext.Customers.SingleOrDefault(customer => customer.Email.ToLower() == model.Email.ToLower());

      customer.Should().NotBeNull();
    }
  }
}