using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Models.MaterialModelDTO;
using Stock_Back.DAL.Context;

namespace Stock_Back.Controllers.MaterialApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MaterialApiController : ControllerBase
    {
        private readonly MaterialResponseController _materialResponseController;

        public MaterialApiController(AppDbContext dbContext, IMapper mapper)
        {
            _materialResponseController = new MaterialResponseController(dbContext, mapper);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterials(int id, int? pageNumber, int? pageSize)
        {
            return await _materialResponseController.GetResponseMaterials(id, pageNumber, pageSize);
        }

        [HttpPost]
        public async Task<IActionResult> InsertMaterial([FromBody] MaterialInsertDTO MaterialType)
        {
            return await _materialResponseController.Insert(MaterialType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMaterial([FromBody] MaterialEditDTO materialEdited)
        {
            return await _materialResponseController.Update(materialEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            return await _materialResponseController.Delete(id);
        }
    }
}
