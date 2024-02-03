using Stock_Back.BLL.Models.UserDTO;
using System.Text;

namespace Stock_Back.BLL.PdfHelper
{
    public class UserHelper
    {
        public static string ToHtmlFile(List<UserDTO> data)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlTemplates", "template.html");
            string tempHtml = File.ReadAllText(templatePath);
            StringBuilder stringData = new StringBuilder(String.Empty);
            for (int i = 0; i < data.Count; i++)
            {
                stringData.Append($"<tr><td>{i + 1}</td>");
                stringData.Append($"<td>{data[i].Id}</td>");
                stringData.Append($"<td>{data[i].Name}</td>");
                stringData.Append($"<td>{data[i].Email}</td>");
                stringData.Append($"<td>{data[i].Phone}</td>");
                stringData.Append($"<td>{data[i].Address}</td>");
                stringData.Append($"<td>{data[i].Created.ToString("dd/MM/yyyy")}</td></tr>");
                stringData.Append($"<td>{data[i].Vigency}</td>");
            }
            return tempHtml.Replace("{data}", stringData.ToString());
        }
    }
}
