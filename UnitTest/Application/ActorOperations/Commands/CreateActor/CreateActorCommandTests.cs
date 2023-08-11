using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.DBOperations;
using MovieStore.Entities;
using TestSetup;
using Xunit;

namespace Application.ActorOperations.Commands.CreateActor
{
  public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly MovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateActorCommandTests(CommonTestFixture testFixture)
    {
      _dbContext = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistingActorNameIsGiven_Handle_ThrowsInvalidOperationException()
    {
      Actor actor = new Actor()
      {
        FirstName = "Existing",
        LastName = "Actor"
      };
      _dbContext.Actors.Add(actor);
      _dbContext.SaveChanges();

      // arrange
      CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
      command.Model = new CreateActorModel()
      {
        FirstName = actor.FirstName,
        LastName = actor.LastName
      };

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Oyuncu zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
    {
      // arrange
      CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
      var model = new CreateActorModel()
      {
        FirstName = "New",
        LastName = "Actor"
      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      var actor = _dbContext.Actors.SingleOrDefault(actor => actor.FirstName.ToLower() == model.FirstName.ToLower() && actor.LastName.ToLower() == model.LastName.ToLower());

      actor.Should().NotBeNull();
      actor.FirstName.Should().Be(model.FirstName);
      actor.LastName.Should().Be(model.LastName);
    }
  }
}