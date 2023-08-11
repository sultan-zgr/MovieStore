using System;
using System.Linq;
using AutoMapper;
using MovieStore.Application.ActorOperations.Queries.GetActorById;
using MovieStore.DBOperations;
using MovieStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.ActorOperations.Queries.GetActorById
{
  public class GetActorByIdQueryTests : IClassFixture<CommonTestFixture>
  {
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetActorByIdQueryTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenActorIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      GetActorByIdQuery query = new GetActorByIdQuery(_context, _mapper);
      query.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => query.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Oyuncu bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeReturned()
    {
      // arrange
      GetActorByIdQuery query = new GetActorByIdQuery(_context, _mapper);
      query.Id = 2;

      // act
      FluentActions.Invoking(() => query.Handle()).Invoke();

      // assert
      Actor author = _context.Actors.SingleOrDefault(author => author.Id == query.Id);

      author.Should().NotBeNull();
    }
  }
}
