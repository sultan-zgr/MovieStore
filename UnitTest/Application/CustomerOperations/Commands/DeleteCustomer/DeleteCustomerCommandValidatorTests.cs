using MovieStore.Application.CustomerOperations.Commands.DeleteCustomer;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.CustomerOperations.Commands.DeleteCustomer
{
  public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      DeleteCustomerCommand command = new DeleteCustomerCommand(null,null);
      command.Id = 0;

      // act
      DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
      var validationResult = validator.Validate(command);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      DeleteCustomerCommand command = new DeleteCustomerCommand(null,null);
      command.Id = 1;

      // act
      DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }
}
