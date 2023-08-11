using System;
using System.Linq;
using AutoMapper;
using MovieStore.DBOperations;
using MovieStore.Entities;

namespace MovieStore.Application.ActorOperations.Commands.CreateActor
{
  public class CreateActorCommand
  {
    public CreateActorModel Model { get; set; }

    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateActorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public void Handle()
    {
      Actor actor = _dbContext.Actors.SingleOrDefault(actor => (actor.FirstName.ToLower() == Model.FirstName.ToLower() && actor.LastName.ToLower() == Model.LastName.ToLower()));
      if (actor is not null)
      {
        throw new InvalidOperationException("Oyuncu zaten mevcut.");
      }

      actor = _mapper.Map<Actor>(Model);

      _dbContext.Actors.Add(actor);
      _dbContext.SaveChanges();
    }
  }
  public class CreateActorModel
  {
    private string firstName;
    public string FirstName
    {
      get { return firstName; }
      set { firstName = value.Trim(); }
    }
    private string lastName;
    public string LastName
    {
      get { return lastName; }
      set { lastName = value.Trim(); }
    }

  }
}