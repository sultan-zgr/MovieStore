using System;
using MovieStore.Application.CustomerOperations.Commands.CreateCustomer;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.CustomerOperations.Commands.CreateCustomer
{
  public class CreateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
  {

    [Theory]
    // email - password - firstName - lastName
    // [InlineData("newcustomer@example.com", "newcustomer", "firstname", "lastname")] - Valid
    [InlineData("", "newcustomer", "firstname", "lastname")]
    [InlineData(" ", "newcustomer", "firstname", "lastname")]
    [InlineData("    ", "newcustomer", "firstname", "lastname")]
    [InlineData("  aaa   ", "newcustomer", "firstname", "lastname")]
    [InlineData("in%va%lid%email.com", "newcustomer", "firstname", "lastname")]
    [InlineData("example.com", "newcustomer", "firstname", "lastname")]
    [InlineData("A@b@c@domain.com", "newcustomer", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", " ", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "   ", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "a", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "ab", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "abc", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "abcd", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "abcde", "firstname", "lastname")]
    [InlineData("newcustomer@example.com", "newcustomer", "", "lastname")]
    [InlineData("newcustomer@example.com", "newcustomer", " ", "lastname")]
    [InlineData("newcustomer@example.com", "newcustomer", "firstname", "")]
    [InlineData("newcustomer@example.com", "newcustomer", "firstname", " ")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string email, string password, string firstName, string lastName)
    {
      // arrange
      CreateCustomerCommand command = new CreateCustomerCommand(null, null);
      command.Model = new CreateCustomerModel()
      {
        Email = email,
        Password = password,
        FirstName = firstName,
        LastName = lastName
      };

      // act
      CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      CreateCustomerCommand command = new CreateCustomerCommand(null, null);
      command.Model = new CreateCustomerModel()
      {
        Email = "newcustomer@example.com",
        Password = "newcustomer",
        FirstName = "firstname",
        LastName = "lastname"
      };

      // act
      CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }
  }
}