using System;
using System.Linq;
using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.DBOperations;
using MovieStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.ActorOperations.Commands.DeleteActor
{
  public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly MovieStoreDbContext _context;
    public DeleteActorCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenActorIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      DeleteActorCommand command = new DeleteActorCommand(_context);
      command.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Oyuncu bulunamadı.");
    }

    [Fact]
    public void WhenGivenActorIsCurrentlyPlayingInAMovie_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      DeleteActorCommand command = new DeleteActorCommand(_context);
      command.Id = 2;

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Bu oyuncu bir filmde oynamaktadır, şu anda silinemez.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeDeleted()
    {
      // arrange
      Actor actorWithNoMovies = new Actor()
      {
        FirstName = "Actor with",
        LastName = "No movie",
      };

      _context.Actors.Add(actorWithNoMovies);
      _context.SaveChanges();

      Actor createdActor = _context.Actors.SingleOrDefault(author => ((author.FirstName.ToLower() + " " + author.LastName.ToLower()) == (actorWithNoMovies.FirstName.ToLower() + " " + actorWithNoMovies.LastName.ToLower())));

      DeleteActorCommand command = new DeleteActorCommand(_context);
      command.Id = createdActor.Id;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Actor author = _context.Actors.SingleOrDefault(author => author.Id == command.Id);

      author.Should().BeNull();
    }
  }
}

