using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Interfaces;
using Stock_Back.Models;
using Stock_Back.UserJwt;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class DeleteUser : ControllerBase
    {
        private readonly AppDbContext _context;
        public DeleteUser(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //TODO: Mover la lógica de la respuesta fuera de la capa de negocio y serparar ambas capas
                //TODO: Validar si el usuario existe
                ResponseType type = ResponseType.Success;
                var deleter = new UserDelete(_context);
                var deleted = await deleter.DeleteUser(id);
                if (!deleted)
                {
                    //TODO: Mover respuestas string a un archivo de config
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with ID {id} not found."));
                }
                return Ok(ResponseHandler.GetAppResponse(type, $"User with ID {id} deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}