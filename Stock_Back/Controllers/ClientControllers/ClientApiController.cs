using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.ClientModelDTO;
using AutoMapper;

namespace Stock_Back.Controllers.ClientApiControllers
{
    [SuperAdminRequired]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientApiController : ControllerBase
    {
        private readonly ClientsResponseController _bll;

        public ClientApiController(AppDbContext dbContext, IMapper mapper)
        {
            _bll = new ClientsResponseController(dbContext, mapper);
        }
        [HttpGet]
        public async Task<IActionResult> GetClientsById(string? value = "", string? identifier = "all")
        {
            //the identifier will be the get by predicate
            //e.g value = admin@admin.cl ; identifier = email
            //e.g value = 0123456789 ; identifier = phone
            //e.g value = 2024-03-31 ; identifier = createddate
            //e.g default identifier = get all clients (regardless of value provided)
            return await _bll.GetResponseClients(value, identifier);
        }
        [HttpGet]
        public async Task<IActionResult> GetClients(string? value = "", string? identifier = "all")
        {
            //the identifier will be the get by predicate
            //e.g value = admin@admin.cl ; identifier = email
            //e.g value = 0123456789 ; identifier = phone
            //e.g value = 2024-03-31 ; identifier = createddate
            //e.g default identifier = get all clients (regardless of value provided)
            return await _bll.GetResponseClients(value, identifier);
        }

        [HttpPost]
        public async Task<IActionResult> InsertClient([FromBody] ClientInsertDTO client)
        {
            if (!ModelState.IsValid || client.TaxId == default) return BadRequest(client);
            return await _bll.Insert(client);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] ClientEditDTO clientEdited)
        {
            if (!ModelState.IsValid || clientEdited.TaxId == default) return BadRequest(clientEdited);
            return await _bll.Update(clientEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            return await _bll.Delete(id);
        }
    }
}