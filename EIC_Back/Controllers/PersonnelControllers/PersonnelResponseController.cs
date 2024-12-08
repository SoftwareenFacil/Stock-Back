using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EIC_Back.BLL.EnumExtensions;
using EIC_Back.Controllers.Services;
using EIC_Back.DAL.Context;
using EIC_Back.Models;
using EIC_Back.BLL.Models.PersonnelModelDTO;
using EIC_Back.BLL.Services;

namespace EIC_Back.Controllers.PersonnelApiControllers
{
    public class PersonnelResponseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ResponseService _responseService;
        private readonly PersonnelService _personnelService;

        public PersonnelResponseController(AppDbContext dbContext, IMapper mapper)
        {
            _personnelService = new PersonnelService(dbContext, mapper);
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> GetResponsePersonnel(string? value, object? identifier, int? pageNumber, int? pageSize)
        {
            Identifier ident = GetEnumValue(identifier);
            var personnel = await _personnelService.GetPersonnel(value, ident, pageNumber, pageSize);

            if (personnel == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                $"Personnel with {identifier} {value} not found."));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(personnel, "Success when searching for Personnel"));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _personnelService.DeletePersonnelById(id);

            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Personnel with id {id} not found"));
            }

            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Personnel with ID {id} deleted successfully", "Delete completed"));
        }
        public async Task<IActionResult> Insert(PersonnelInsertDTO personnel)
        {
            var dataModified = await _personnelService.AddPersonnel(personnel);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(dataModified, $"Personnel with Name {personnel.Name} created succesfully Create completed"));
            else if (dataModified == -1)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(personnel, $"Personnel with mail {personnel.Email} already exists"));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to create a Personnel"));
        }
        public async Task<IActionResult> Update(PersonnelEditDTO personnelEdited)
        {
            var (isUpdated, isPersonnel) = await _personnelService.UpdatePersonnel(personnelEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"Personnel with ID {personnelEdited.Id} updated", "Update completed"));
            else if (!isPersonnel)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"Personnel with ID {personnelEdited.Id} not found."));

            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update Personnel"));
        }

        private static Identifier GetEnumValue(object? value)
        {
            if (value is int id)
            {
                return (Identifier)id;
            }
            else if (value is string str)
            {
                return (Identifier)Enum.Parse(typeof(Identifier), str, true);
            }
            else
            {
                return Identifier.Unknown;
            }
        }
    }
}
