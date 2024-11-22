using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonAPI.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PersonDbContext _dbContext;
        protected readonly DbSet<T> _set;
        protected readonly IMapper _mapper;
        public BaseRepository(PersonDbContext db, IMapper mapper)
        {
            _dbContext = db;
            _set = _dbContext.Set<T>();
            _mapper = mapper;
        }
        public virtual async Task Add(T entity)
        {
            await _set.AddAsync(entity);
        }

        public virtual async Task Delete(T entity)
        {
            _set.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _set.AsNoTracking();
        }

        public async Task<T> GetById(params object[] ids)
        {
            return await _set.FindAsync(ids);
        }

        public virtual async Task Update(T entity)
        {
            _set.Update(entity);
        }
    }
}
