using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using PersonAPI.Core.Contracts;
using PersonAPI.Core.Entity;
using PersonAPI.Core.Models;
using PersonAPI.Core.Models.Pagination;
using PersonAPI.Core.Models.Report;
using PersonAPI.Core.Resources.Person;
using PersonAPI.Core.Resources.RelatedPerson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService; 
        }
        public IQueryable<PersonModel> GetAll()
        {
            var entities = _unitOfWork.PersonRepository.GetAll();
            return entities.Select(entity => _mapper.Map<PersonModel>(entity));
        }
        public async Task Add(PersonModel model)
        {
            if (await _unitOfWork.PersonRepository.PersonExists(model.PersonalNumber))
                throw new Exception(PersonMessage.PersonAlreadyExists);

            await _unitOfWork.PersonRepository.Add(_mapper.Map<PersonEntity>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(PersonModel model)
        {
            var entity = await GetEntityById(model.Id, false);
            await _unitOfWork.PersonRepository.Update(_mapper.Map(model, entity));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetEntityById(id, false);
            await _unitOfWork.PersonRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PersonModel> GetById(int id)
        {
            var entity = await GetEntityById(id, false);
            return _mapper.Map<PersonModel>(entity);
        }

        public async Task<PersonModel> GetPersonById(int id)
        {
            var entity = await GetEntityById(id, false);
            return await _unitOfWork.PersonRepository.GetPersonById(id);
        }
        public async Task<PagedResult<PersonModel>> GetPersons(PersonFilter filter) => await _unitOfWork.PersonRepository.GetPersons(filter);
        public async Task<List<RelatedPersonsReport>> GetRelatedPersonsReport() => await _unitOfWork.PersonRepository.GetRelatedPersonsReport();
        public async Task<List<CityModel>> GetCities() => await _unitOfWork.PersonRepository.GetCities();

        public async Task AddRelatedPerson(int id, RelatedPersonModel relation)
        {
            await GetEntityById(id, false);
            await GetEntityById(relation.RelatedPersonId, true);

            var relatedPersonEntity = _mapper.Map<RelatedPersonEntity>(relation);
            relatedPersonEntity.PersonId = id;

            await _unitOfWork.RelatedPersonRepository.Add(relatedPersonEntity);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteRelatedPerson(int id)
        {
            var entity = await _unitOfWork.RelatedPersonRepository.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException(RelatedPersonMessage.RelatedPersonIdInvalid);

            await _unitOfWork.RelatedPersonRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        private async Task<PersonEntity> GetEntityById(int id, bool isRelatedPerson)
        {
            var entity = await _unitOfWork.PersonRepository.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException(isRelatedPerson ? RelatedPersonMessage.RelatedPersonIdInvalid : PersonMessage.PersonNotFound);

            return entity;
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var url = await _fileService.UploadImageAsync(ms);

            return url; 
        }

    }
}
