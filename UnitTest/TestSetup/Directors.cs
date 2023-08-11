using System.Collections.Generic;
using MovieStore.DBOperations;
using MovieStore.Entities;

namespace TestSetup
{
  public static class Directors
  {
    public static void AddDirectors(this MovieStoreDbContext context)
    {
      context.Directors.AddRange(
        new Director { FirstName = "Quentin", LastName = "Tarantino" },
        new Director { FirstName = "Michael", LastName = "Haneke" },
        new Director { FirstName = "Charlie", LastName = "Kaufman" }
      );
    }
  }
}