using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EIC_Back.Controllers.Services;
using EIC_Back.DAL.Context;
using EIC_Back.Models;
using EIC_Back.BLL.Models.FinancialMovementsModelDTO;
using EIC_Back.BLL.Services;
using EIC_Back.DAL.Models;

namespace EIC_Back.Controllers.FinancialMovementsApiControllers
{
    public class FinancialMovementsResponseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ResponseService _responseService;
        private readonly FinancialMovementService _financialMovementService;

        public FinancialMovementsResponseController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _responseService = new ResponseService();
            _financialMovementService = new FinancialMovementService(_dbContext, _mapper);
            
        }

        public async Task<IActionResult> GetResponseFinancialMovements(int id, int? pageNumber, int? pageSize)
        {
            var FinancialMovements = await _financialMovementService.GetFinancialMovements(id, pageNumber, pageSize);

            if (FinancialMovements == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no FinancialMovements." : $"Activity with id {id} not found."));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(FinancialMovements, "Success when searching for FinancialMovements"));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _financialMovementService.DeleteFinancialMovementsById(id);

            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"FinancialMovements with id {id} not found"));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"FinancialMovements with ID {id} deleted successfully", "Delete completed"));
        }
        public async Task<IActionResult> Insert(FinancialMovementsInsertDTO FinancialMovements)
        {
            var dataModified = await _financialMovementService.AddFinancialMovements(FinancialMovements);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(dataModified, $"FinancialMovements with Name {FinancialMovements.Name} created succesfully Create completed"));
            else if (dataModified <= 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(FinancialMovements, $"FinancialMovements with Name {FinancialMovements.Name} already exists"));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a FinancialMovements"));
        }
        public async Task<IActionResult> Update(FinancialMovementsEditDTO FinancialMovementsEdited)
        {
            var (isUpdated, isFinancialMovements) = await _financialMovementService.UpdateFinancialMovements(FinancialMovementsEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"FinancialMovements with ID {FinancialMovementsEdited.Id} updated", "Update completed"));
            else if (!isFinancialMovements)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"FinancialMovements with ID {FinancialMovementsEdited.Id} not found."));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update FinancialMovements"));
        }
    }
}
