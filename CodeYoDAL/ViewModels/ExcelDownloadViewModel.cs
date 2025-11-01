using CodeYoDAL.Models;
using ShaghalnyDAL.Models;

namespace CodeYoDAL.ViewModels
{
    public class ExcelDownloadViewModel
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] DocByte { get; set; }
    }
}
