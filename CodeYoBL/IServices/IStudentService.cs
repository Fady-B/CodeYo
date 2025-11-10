using CodeYoDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoBL.IServices
{
    public interface IStudentService
    {
        Task<StudentsViewModel> GetAsync(Guid Id);
        Task<JsonResultViewModel> AddEditAsync(StudentsViewModel vm);
        Task<JsonResultViewModel> DeleteAsync(Guid Id);

        Task<StudentsViewModel> GetFirstSudentAsync();
    }
}
