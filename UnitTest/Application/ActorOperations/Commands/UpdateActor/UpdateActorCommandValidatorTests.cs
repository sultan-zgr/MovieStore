using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.ActorOperations.Commands.UpdateActor
{
  public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    /* 
      Boş ya da 1 karakterden uzun her string 
      valid olduğu için invalid case yok.
    */

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      UpdateActorCommand command = new UpdateActorCommand(null);
      command.Id = 1;
      command.Model = new UpdateActorModel()
      {
        FirstName = "Updated",
        LastName = "Actor Name"
      };

      // act
      UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);

    }
  }

}