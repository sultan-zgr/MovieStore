using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.ActorOperations.Commands.DeleteActor
{
  public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      DeleteActorCommand command = new DeleteActorCommand(null);
      command.Id = 0;

      // act
      DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
      var validationResult = validator.Validate(command);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      DeleteActorCommand command = new DeleteActorCommand(null);
      command.Id = 1;

      // act
      DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }
}
