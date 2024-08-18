using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;
using Stock_Back.BLL.Models.FinancialCategoryModelDTO;
using Stock_Back.BLL.Services;

namespace Stock_Back.Controllers.FinancialCategoryApiControllers
{
    public class FinancialCategoryResponseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ResponseService _responseService;
        private readonly FinancialCategoryService _financialCategoryService;

        public FinancialCategoryResponseController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _responseService = new ResponseService();
            _financialCategoryService = new FinancialCategoryService(dbContext, _mapper);
        }

        public async Task<IActionResult> GetResponseFinancialCategory(int id, int? pageNumber, int? pageSize)
        {
            var FinancialCategory = await _financialCategoryService.GetFinancialCategory(id, pageNumber, pageSize);

            if (FinancialCategory == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no FinancialCategory." : $"Activity with id {id} not found."));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(FinancialCategory, "Success when searching for FinancialCategory"));
        }
        public async Task<IActionResult> GetFullResponseFinancialCategory(int id, int? pageNumber, int? pageSize)
        {
            var FinancialCategory = await _financialCategoryService.GetAllFinancialCategory(id, pageNumber, pageSize);

            if (!FinancialCategory.Any())
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no FinancialCategory." : $"Activity with id {id} not found."));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(FinancialCategory, "Success when searching for FinancialCategory"));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _financialCategoryService.DeleteFinancialCategoryById(id);

            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"FinancialCategory with id {id} not found"));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"FinancialCategory with ID {id} deleted successfully", "Delete completed"));
        }
        public async Task<IActionResult> Insert(FinancialCategoryInsertDTO FinancialCategory)
        {
            var FinancialCategoryCreator = new FinancialCategoryService(_dbContext, _mapper);
            var dataModified = await FinancialCategoryCreator.AddFinancialCategory(FinancialCategory);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(dataModified, $"FinancialCategory with Name {FinancialCategory.Name} created succesfully, Create completed!"));
            else if (dataModified <= 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(FinancialCategory, $"FinancialSubCategory with Name {FinancialCategory.Name} already exists"));


            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a FinancialCategory"));
        }
        public async Task<IActionResult> Update(FinancialCategoryEditDTO FinancialCategoryEdited)
        {
            var (isUpdated, isFinancialCategory) = await _financialCategoryService.UpdateFinancialCategory(FinancialCategoryEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"FinancialCategory with ID {FinancialCategoryEdited.Id} updated", "Update completed"));
            else if (!isFinancialCategory)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"FinancialCategory with ID {FinancialCategoryEdited.Id} not found."));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update FinancialCategory"));
        }
    }
}
