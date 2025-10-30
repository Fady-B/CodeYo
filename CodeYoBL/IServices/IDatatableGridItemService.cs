using CodeYoDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoBL.IServices
{
    public interface IDatatableGridItemService
    {
        IQueryable<TeacherViewModel> GetTeachersGridItem();
        Task<TeacherViewModel> GetTeacherAsync(Guid TeacherId);
        Task<JsonResultViewModel> AddEditTeacherAsync(TeacherViewModel vm);
        Task<JsonResultViewModel> DeleteTeacherAsync(Guid TeacherId);
    }
}
