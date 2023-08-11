using MovieStore.Application.ActorOperations.Queries.GetActorById;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.ActorOperations.Queries.GetActorById
{
  public class GetActorByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      GetActorByIdQuery query = new GetActorByIdQuery(null, null);
      query.Id = 0;

      // act
      GetActorByIdQueryValidator validator = new GetActorByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      GetActorByIdQuery query = new GetActorByIdQuery(null, null);
      query.Id = 1;

      // act
      GetActorByIdQueryValidator validator = new GetActorByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }

}