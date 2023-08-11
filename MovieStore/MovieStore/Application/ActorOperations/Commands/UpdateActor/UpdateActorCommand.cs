using System;
using System.Linq;
using MovieStore.DBOperations;
using MovieStore.Entities;

namespace MovieStore.Application.ActorOperations.Commands.UpdateActor
{
  public class UpdateActorCommand
  {
    public int Id { get; set; }
    public UpdateActorModel Model { get; set; }
    private readonly IMovieStoreDbContext _dbContext;
    public UpdateActorCommand(IMovieStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Actor actor = _dbContext.Actors.SingleOrDefault(actor => actor.Id == Id);
      if (actor is null)
      {
        throw new InvalidOperationException("Oyuncu bulunamadı.");
      }

      actor.FirstName = string.IsNullOrEmpty(Model.FirstName) ? actor.FirstName : Model.FirstName.Trim();
      actor.LastName = string.IsNullOrEmpty(Model.LastName) ? actor.LastName : Model.LastName.Trim();

      if (_dbContext.Actors.Any(a => a.FirstName.ToLower() == actor.FirstName.ToLower() && a.LastName.ToLower() == actor.LastName.ToLower() && a.Id != actor.Id))
      {
        throw new InvalidOperationException("Bu isimde bir oyuncu zaten var.");
      }
      _dbContext.SaveChanges();
    }
  }

  public class UpdateActorModel
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