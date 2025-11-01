using CodeYoDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoBL.IServices
{
    public interface ICommonService
    {
        IQueryable<ItemDropdownListViewModel> GetTeachersDdl();
    }
}
