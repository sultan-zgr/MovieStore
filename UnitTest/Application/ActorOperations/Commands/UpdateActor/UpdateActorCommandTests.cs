using System;
using System.Linq;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.DBOperations;
using MovieStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.ActorOperations.Commands.UpdateActor
{
  public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly MovieStoreDbContext _context;
    public UpdateActorCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenActorIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      UpdateActorCommand command = new UpdateActorCommand(_context);
      command.Id = 999;
      command.Model = new UpdateActorModel() { FirstName = "Updated", LastName = "Actor" };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Oyuncu bulunamadı.");
    }

    [Fact]
    public void WhenGivenActorNameAlreadyExistsWithDifferentId_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      UpdateActorCommand command = new UpdateActorCommand(_context);
      command.Id = 3;
      command.Model = new UpdateActorModel() { FirstName = "Alison", LastName = "Brie" };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Bu isimde bir oyuncu zaten var.");
    }

    [Fact]
    public void WhenDefaultInputsAreGiven_Actor_ShouldNotBeChanged()
    {
      // arrange
      UpdateActorCommand command = new UpdateActorCommand(_context);
      command.Id = 3;
      UpdateActorModel model = new UpdateActorModel()
      {
        FirstName = "",
        LastName = ""
      };
      command.Model = model;

      Actor authorBeforeUpdate = _context.Actors.SingleOrDefault(author => author.Id == command.Id);

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Actor authorAfterUpdate = _context.Actors.SingleOrDefault(author => author.Id == command.Id);

      authorAfterUpdate.Should().NotBeNull();
      authorAfterUpdate.FirstName.Should().Be(authorBeforeUpdate.FirstName);
      authorAfterUpdate.LastName.Should().Be(authorBeforeUpdate.LastName);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
    {
      // arrange
      UpdateActorCommand command = new UpdateActorCommand(_context);
      command.Id = 3;
      UpdateActorModel model = new UpdateActorModel()
      {
        FirstName = "Updated",
        LastName = "Actor Name",

      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Actor author = _context.Actors.SingleOrDefault(author => author.Id == command.Id);

      author.Should().NotBeNull();
      author.FirstName.Should().Be(model.FirstName);
      author.LastName.Should().Be(model.LastName);
    }
  }
}