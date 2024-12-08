using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EIC_Back.BLL.Models.PersonnelModelDTO;
using EIC_Back.DAL.Context;

namespace EIC_Back.Controllers.PersonnelApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonnelApiController : ControllerBase
    {
        private readonly PersonnelResponseController _responseController;

        public PersonnelApiController(AppDbContext dbContext, IMapper mapper)
        {
            _responseController = new PersonnelResponseController(dbContext, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonnel(string? value, string? identifier = "all", int? pageNumber = null, int? pageSize = null)
        {
            //the identifier will be the get by predicate
            //e.g value = admin@admin.cl ; identifier = email
            //e.g value = 0123456789 ; identifier = phone
            //e.g value = 2024-03-31 ; identifier = createddate
            //e.g default identifier = get all clients (regardless of value provided)
            return await _responseController.GetResponsePersonnel(value, identifier, pageNumber, pageSize);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPersonnel([FromBody] PersonnelInsertDTO personnel)
        {
            if (!ModelState.IsValid || personnel.TaxId == default) return BadRequest(personnel);
            return await _responseController.Insert(personnel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePersonnel([FromBody] PersonnelEditDTO personnelEdited)
        {
            if (!ModelState.IsValid || personnelEdited.TaxId == default) return BadRequest(personnelEdited);
            return await _responseController.Update(personnelEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePersonnel(int id)
        {
            return await _responseController.Delete(id);
        }
    }
}
