using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace CodeYoDAL.DALHelpers
{
    public class ExcelFileHelper
    {
        public async Task<DataSet> ReadExcelFile(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return reader.AsDataSet();
                }
            }
        }
    }
}
