using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;
using Stock_Back.BLL.Models.MaterialModelDTO;
using Stock_Back.BLL.Services;

namespace Stock_Back.Controllers.MaterialApiControllers
{
    public class MaterialResponseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ResponseService _responseService;
        private readonly MaterialService _materialService;

        public MaterialResponseController(AppDbContext dbContext, IMapper mapper)
        {
            _materialService = new MaterialService(dbContext, mapper);
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> GetResponseMaterials(int id, int? pageNumber, int? pageSize)
        {
            var materials = await _materialService.GetMaterials(id, pageNumber, pageSize);

            if (materials == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no Materials." : $"MaterialType with id {id} not found."));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(materials, "Success when searching for Materials"));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _materialService.DeleteMaterialById(id);

            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"MaterialType with id {id} not found"));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"MaterialType with ID {id} deleted successfully", "Delete completed"));
        }
        public async Task<IActionResult> Insert(MaterialInsertDTO MaterialType)
        {
            var dataModified = await _materialService.AddMaterial(MaterialType);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(dataModified, $"MaterialType with Name {MaterialType.Name} created succesfully Create completed"));
            else if (dataModified <= 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(MaterialType, $"MaterialType with Name {MaterialType.Name} already exists"));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a MaterialType"));
        }
        public async Task<IActionResult> Update(MaterialEditDTO materialEdited)
        {
            var (isUpdated, isMaterial) = await _materialService.UpdateMaterial(materialEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"MaterialType with ID {materialEdited.Id} updated", "Update completed"));
            else if (!isMaterial)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"MaterialType with ID {materialEdited.Id} not found."));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update MaterialType"));
        }
    }
}
