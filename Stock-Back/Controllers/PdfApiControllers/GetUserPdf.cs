using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Controllers.PdfControllers;
using Stock_Back.DAL.Models;
using Stock_Back.Helper;
using Stock_Back.Models;
using System;


namespace Stock_Back.Controllers.PdfApiControllers
{
    public class GetUserPdf : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConverter _convert;
        private readonly ResponseService _responseService;
        public GetUserPdf(IConverter convert, AppDbContext dbContext)
        {
            _convert = convert;
            _context = dbContext;
            _responseService = new ResponseService();
        }

        public async Task<IActionResult> GetUserPdfBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new GetUsersController(_context);
            var users = await userGetter.GetUsersBy(id, name, email, created, vigency);
            if (users.Count() > 0)
            {
                var pdfGetter = new GetPdfController(_convert, _context);
                var filePath = await pdfGetter.GetPdfPath(users);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("El archivo PDF no fue encontrado.");
                }
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, "application/pdf", "Users.pdf");
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse("There are no users with these parameters"));
        }
    }
}
