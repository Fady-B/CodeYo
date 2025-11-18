using CodeYoDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoBL.IServices
{
    public interface ICardsService
    {
        Task<TeacherViewModel> GetAsync(Guid TeacherId);
        Task<JsonResultViewModel> AddEditAsync(TeacherViewModel vm);
        Task<JsonResultViewModel> DeleteAsync(Guid TeacherId);
    }
}
