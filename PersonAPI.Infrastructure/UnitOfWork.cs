using Microsoft.Extensions.DependencyInjection;
using PersonAPI.Core;
using PersonAPI.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Infrastructure
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly IServiceProvider _services;
    private readonly PersonDbContext _dbContext;
    public UnitOfWork(PersonDbContext dbContext, IServiceProvider services)
    {
      _dbContext = dbContext;
      _services = services;
    }
    public IPersonRepository PersonRepository => _services.GetService<IPersonRepository>();
    public IRelatedPersonRepository RelatedPersonRepository => _services.GetService<IRelatedPersonRepository>();
    public async Task<int> SaveAsync()
    {
      return await _dbContext.SaveChangesAsync();
    }
  }
}
