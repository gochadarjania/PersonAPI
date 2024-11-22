using Microsoft.EntityFrameworkCore;
using PersonAPI.Core.Contracts.Repositories;
using PersonAPI.Core.Entity;
using PersonAPI.Core.Models.Pagination;
using PersonAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using PersonAPI.Core.Models.Report;

namespace PersonAPI.Infrastructure.Repositories
{
    public class PersonRepository : BaseRepository<PersonEntity>, IPersonRepository
    {
        public PersonRepository(PersonDbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task<PagedResult<PersonModel>> GetPersons(PersonFilter filter)
        {
            IQueryable<PersonEntity> query = _dbContext.Persons;

            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                query = query.Where(person =>
                    EF.Functions.Like(person.FirstName, $"%{filter.SearchQuery}%") ||
                    EF.Functions.Like(person.LastName, $"%{filter.SearchQuery}%") ||
                    EF.Functions.Like(person.PersonalNumber, $"%{filter.SearchQuery}%"));
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(person => EF.Functions.Like(person.FirstName, $"%{filter.Name}%"));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(person => EF.Functions.Like(person.LastName, $"%{filter.LastName}%"));
            }

            if (!string.IsNullOrEmpty(filter.PersonalNumber))
            {
                query = query.Where(person => EF.Functions.Like(person.PersonalNumber, $"%{filter.PersonalNumber}%"));
            }

            if (filter.CityId.HasValue)
            {
                query = query.Where(person => person.CityId == filter.CityId);
            }

            if (filter.Gender.HasValue)
            {
                query = query.Where(person => person.Gender == filter.Gender.Value);
            }

            if (filter.BirthDateFrom.HasValue)
            {
                query = query.Where(person => person.BirthDate >= filter.BirthDateFrom.Value);
            }

            if (filter.BirthDateTo.HasValue)
            {
                query = query.Where(person => person.BirthDate <= filter.BirthDateTo.Value);
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(person => person.LastName)
                .ThenBy(person => person.FirstName)
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ProjectTo<PersonModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<PersonModel>(items, totalCount, filter.PageIndex, filter.PageSize);
        }
        public async Task<List<RelatedPersonsReport>> GetRelatedPersonsReport()
        {
            var report = await _dbContext.Persons
                .AsNoTracking() 
                .SelectMany(p => _dbContext.RelatedPersons
                    .Where(rp => rp.PersonId == p.Id)
                    .GroupBy(rp => rp.RelationshipType)
                    .Select(group => new RelatedPersonsReport
                    {
                        PersonId = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        RelationshipType = group.Key,
                        RelatedPersonCount = group.Count()
                    }))
                .ToListAsync();

            return report;
        }
        public async Task<PersonModel> GetPersonById(int id)
        {
            var person = await _dbContext.Persons
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<PersonModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return person;
        }
        public async Task<bool> PersonExists(string personalNumber)
        {
            return await _dbContext.Persons
                .AsNoTracking()
                .AnyAsync(p => p.PersonalNumber == personalNumber);
        }
        public async Task<List<CityModel>> GetCities()
        {
            var cities = await _dbContext.Cities
                .AsNoTracking()
                .ProjectTo<CityModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return cities;
        }
    }
}
