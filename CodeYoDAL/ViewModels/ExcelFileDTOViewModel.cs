using Microsoft.AspNetCore.Http;

namespace CodeYoDAL.ViewModels
{
    public class ExcelFileDTOViewModel
    {
        public IFormFile ExcelFileInfo { get; set; }
        public List<Guid> TeachersIds { get; set; }
    }
}
