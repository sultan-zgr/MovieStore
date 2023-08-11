using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
  public class Order
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int Price { get; set; }
    public DateTime ProcessDate { get; set; }
  }
}