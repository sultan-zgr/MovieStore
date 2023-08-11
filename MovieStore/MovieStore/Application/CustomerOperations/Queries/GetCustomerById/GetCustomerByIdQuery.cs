using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.CustomerOperations.Queries.SharedViewModels;
using MovieStore.DBOperations;
using MovieStore.Entities;

namespace MovieStore.Application.CustomerOperations.Queries.GetCustomerById
{
  public class GetCustomerByIdQuery
  {
    public int Id { get; set; }
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCustomerByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public GetCustomerByIdViewModel Handle()
    {
      Customer customer = _dbContext.Customers.Where(customer => customer.Id == Id).Include(customer => customer.FavoriteGenres).Include(customer => customer.Orders).ThenInclude(order => order.Movie).SingleOrDefault();
      if (customer is null)
      {
        throw new InvalidOperationException("Müşteri bulunamadı.");
      }
      GetCustomerByIdViewModel movieVM = _mapper.Map<GetCustomerByIdViewModel>(customer);
      return movieVM;
    }
  }

  public class GetCustomerByIdViewModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<OrderViewModel> Orders { get; set; }
    public List<GenreViewModel> FavoriteGenres { get; set; }
  }

}