using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonAPI.Core.Models;
using PersonAPI.Core.Models.Pagination;
using PersonAPI.Core.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Contracts
{
  public interface IPersonService
    {
        IQueryable<PersonModel> GetAll();
        Task Add(PersonModel model);
        Task AddRelatedPerson(int id, RelatedPersonModel relation);
        Task Update(PersonModel model);
        Task Delete(int id);
        Task DeleteRelatedPerson(int id);
        Task<PersonModel> GetById(int id);
        Task<PersonModel> GetPersonById(int id);
        Task<PagedResult<PersonModel>> GetPersons(PersonFilter filter);
        Task<List<RelatedPersonsReport>> GetRelatedPersonsReport();
        Task<string> UploadImage(IFormFile image);
    }
}
