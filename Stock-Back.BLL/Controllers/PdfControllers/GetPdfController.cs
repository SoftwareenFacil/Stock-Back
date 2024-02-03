using DinkToPdf.Contracts;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.UserDTO;
using Stock_Back.BLL.PdfHelper;
using DinkToPdf;

namespace Stock_Back.BLL.Controllers.PdfControllers
{
    public class GetPdfController
    {
        private readonly IConverter _convert;
        private AppDbContext _context;

        public GetPdfController(IConverter convert, AppDbContext dbContext)
        {
            _convert = convert;
            _context = dbContext;
        }

        public async Task<string?> GetPdfPath(List<UserDTO> users)
        {
            string exportPath = Path.Combine(Directory.GetCurrentDirectory(), "Exports");
            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }
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
                DocumentTitle = "Users",
                Out = Path.Combine(Directory.GetCurrentDirectory(), "Exports", fileName)
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = UserHelper.ToHtmlFile(users),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
            };



            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = glb,
                Objects = { objectSettings }
            };
            var pdfFile = _convert.Convert(pdf);

            // Especifica la ruta del archivo PDF generado
            var filePath = glb.Out;

            return filePath;
        }
    }


}
