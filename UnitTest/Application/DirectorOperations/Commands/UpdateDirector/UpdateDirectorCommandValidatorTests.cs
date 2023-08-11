using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.DirectorOperations.Commands.UpdateDirector
{
  public class UpdateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    /* 
      Boş ya da 1 karakterden uzun her string 
      valid olduğu için invalid case yok.
    */

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      UpdateDirectorCommand command = new UpdateDirectorCommand(null);
      command.Id = 1;
      command.Model = new UpdateDirectorModel()
      {
        FirstName = "Updated",
        LastName = "Director Name"
      };

      // act
      UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);

    }
  }

}