using CodeYoBL.IServices;
using CodeYoDAL.Data;
using CodeYoDAL.Models;
using CodeYoDAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace CodeYoBL.Services
{
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommonService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IQueryable<ItemDropdownListViewModel> GetTeachersDdl()
        {
            return (from tblObj in _context.Teachers
                    where !tblObj.Cancelled
                    select new ItemDropdownListViewModel
                    {
                        Id = tblObj.Id,
                        Name = tblObj.FullName
                    }).OrderBy(x => x.Name);
        }

        public async Task<CountersViewModel> GetCountersAsync()
        {
            CountersViewModel Counts = new();
            try
            {
                Counts.TeachersCount = await _context.Teachers.Where(t => !t.Cancelled).CountAsync();
                Counts.StudentsCount = await _context.Students.Where(t => !t.Cancelled).CountAsync();

                return Counts;
            }
            catch (Exception)
            {
                return Counts;
            }
            
        }

    }
}
