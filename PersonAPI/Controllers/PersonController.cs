using Microsoft.AspNetCore.Mvc;
using PersonAPI.Core.Contracts;
using PersonAPI.Core.Models;
using PersonAPI.Core.Models.Pagination;

namespace PersonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_personService.GetAll().ToList());

        [HttpGet]
        public async Task<IActionResult> GetById(int id) => Ok(await _personService.GetById(id));
        
        [HttpGet]
        public async Task<IActionResult> GetPersonById(int id) => Ok(await _personService.GetPersonById(id));

        [HttpGet]
        public async Task<IActionResult> GetPersons([FromQuery] PersonFilter filter) => Ok(await _personService.GetPersons(filter));

        [HttpGet]
        public async Task<IActionResult> GetRelatedPersonsReport() => Ok(await _personService.GetRelatedPersonsReport());


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PersonModel model)
        {
            await _personService.Add(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonModel model)
        {
            await _personService.Update(model);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddRelatedPerson(int id, [FromBody] RelatedPersonModel relation)
        {
            await _personService.AddRelatedPerson(id, relation);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image) => Ok(await _personService.UploadImage(image));


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _personService.Delete(id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRelatedPerson(int id)
        {
            await _personService.DeleteRelatedPerson(id);
            return Ok();
        }

    }
}
