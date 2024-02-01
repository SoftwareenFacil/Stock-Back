using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock_Back.Helper;
using DinkToPdf;
using DinkToPdf.Contracts;
using System;

namespace Stock_Back.Controllers.PdfControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IConverter _convert;

        public PdfController(IConverter convert)
        {
            _convert = convert;
        }
        [HttpGet]
        public async Task<IActionResult> GeneratePdf()
        {
            string fileName = "Users.pdf";
            var glb = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings()
                {
                    Bottom = 20,
                    Left = 20,
                    Right = 20,
                    Top = 30
                },
                DocumentTitle = "Persons",
                Out = Path.Combine(Directory.GetCurrentDirectory(), "Exports", fileName)
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = UserHelper.ToHtmlFile(Person.GetData()),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
            };
            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = glb,
                Objects = { objectSettings }
            };
            _convert.Convert(pdf);
            string result = $"Files{fileName}";
            await Task.Yield();
            return Ok(result);
        }
    }
}
