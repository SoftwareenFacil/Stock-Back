using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;
using Stock_Back.BLL.Models.FinancialSubCategoryModelDTO;
using Stock_Back.BLL.Services;

namespace Stock_Back.Controllers.FinancialSubCategoryApiControllers
{
    public class FinancialSubcategoryResponseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ResponseService _responseService;
        private readonly FinancialSubcategoryService _financialSubcategoryService;

        public FinancialSubcategoryResponseController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _responseService = new ResponseService();
            _financialSubcategoryService = new FinancialSubcategoryService(dbContext, _mapper);
        }

        public async Task<IActionResult> GetResponseFinancialSubCategory(int id, int? pageNumber, int? pageSize)
        {
            var FinancialSubCategory = await _financialSubcategoryService.GetFinancialSubCategory(id, pageNumber, pageSize);

            if (FinancialSubCategory == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no FinancialSubCategory." : $"Activity with id {id} not found."));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(FinancialSubCategory, "Success when searching for FinancialSubCategory"));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _financialSubcategoryService.DeleteFinancialSubCategoryById(id);

            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"FinancialSubCategory with id {id} not found"));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"FinancialSubCategory with ID {id} deleted successfully", "Delete completed"));
        }
        public async Task<IActionResult> Insert(FinancialSubCategoryInsertDTO FinancialSubCategory)
        {
            var dataModified = await _financialSubcategoryService.AddFinancialSubCategory(FinancialSubCategory);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(dataModified, $"FinancialSubCategory with Name {FinancialSubCategory.Name} created succesfully Create completed!"));
            else if (dataModified <= 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(FinancialSubCategory, $"FinancialSubCategory with Name {FinancialSubCategory.Name} already exists"));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a FinancialSubCategory"));
        }
        public async Task<IActionResult> Update(FinancialSubCategoryEditDTO FinancialSubCategoryEdited)
        {
            var (isUpdated, isFinancialSubCategory) = await _financialSubcategoryService.UpdateFinancialSubCategory(FinancialSubCategoryEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"FinancialSubCategory with ID {FinancialSubCategoryEdited.Id} updated", "Update completed"));
            else if (!isFinancialSubCategory)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"FinancialSubCategory with ID {FinancialSubCategoryEdited.Id} not found."));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update FinancialSubCategory"));
        }
    }
}
