using PersonAPI.Core.Entity;
using PersonAPI.Core.Models.Pagination;
using PersonAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonAPI.Core.Models.Report;

namespace PersonAPI.Core.Contracts.Repositories
{
    public interface IPersonRepository : IBaseRepository<PersonEntity>
    {
        Task<PagedResult<PersonModel>> GetPersons(PersonFilter filter);
        Task<List<RelatedPersonsReport>> GetRelatedPersonsReport();
        Task<PersonModel> GetPersonById(int id);
        Task<bool> PersonExists(string personalNumber);
        Task<List<CityModel>> GetCities();
    }
}
