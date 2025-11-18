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
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CardsService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JsonResultViewModel> AddEditAsync(TeacherViewModel vm)
        {
            JsonResultViewModel _Result = new();
            try
            {
                Teachers _Teacher = new();
                string _UserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                if (vm.Id != Guid.Empty) //Edit
                {
                    _Teacher = await _context.Teachers.FindAsync(vm.Id);
                    vm.CreatedDate = _Teacher.CreatedDate;
                    vm.CreatedBy = _Teacher.CreatedBy;
                    vm.ModifiedDate = DateTime.Now;
                    vm.ModifiedBy = _UserName;

                    _context.Entry(_Teacher).CurrentValues.SetValues(vm);
                    await _context.SaveChangesAsync();

                    _Result.AlertMessage = $"Teacher updated successfully with the name of: {_Teacher.FullName}.";
                }
                else //Add
                {
                    vm.Id = Guid.NewGuid();
                    _Teacher = vm;
                    _Teacher.CreatedDate = DateTime.Now;
                    _Teacher.ModifiedDate = DateTime.Now;
                    _Teacher.CreatedBy = _UserName;
                    _Teacher.ModifiedBy = _UserName;

                    _context.Add(_Teacher);
                    await _context.SaveChangesAsync();

                    _Result.AlertMessage = $"Teacher has been added successfully with the name of: {_Teacher.FullName}.";
                }
                _Result.IsSuccess = true;
                return _Result;
            }
            catch (Exception ex)
            {
                _Result.IsSuccess = false;
                _Result.AlertMessage = "Operation Failed!";
                return _Result;
                throw;
            }
        }

        public async Task<JsonResultViewModel> DeleteAsync(Guid Id)
        {
            JsonResultViewModel _Result = new();
            try
            {
                var _Teacher = await _context.Teachers.FindAsync(Id);

                if (_Teacher == null)
                {
                    _Result.IsSuccess = false;
                    _Result.AlertMessage = "Operation Failed!";
                    return _Result;
                }

                string UserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                _Teacher.ModifiedDate = DateTime.Now;
                _Teacher.ModifiedBy = UserName;
                _Teacher.Cancelled = true;

                var _TeacherStudents = _context.TeacherStudents.Where(x => x.TeacherId == Id);
                _context.TeacherStudents.RemoveRange(_TeacherStudents);

                _context.Update(_Teacher);
                await _context.SaveChangesAsync();

                _Result.AlertMessage = $"The teacher: '{_Teacher.FullName}' has been deleted successfully.";
                _Result.IsSuccess = true;
                return _Result;
            }
            catch (Exception)
            {
                _Result.IsSuccess = false;
                _Result.AlertMessage = "Operation Failed!";
                return _Result;
                throw;
            }
        }

        public async Task<TeacherViewModel> GetAsync(Guid Id)
        {
            var _teacher = await _context.Teachers
                .Include(s => s.TeacherStudents)
                .ThenInclude(ts => ts.Student)
                .Where(t => t.Id == Id && !t.Cancelled)
                .Select(t => new TeacherViewModel
                {
                    Id = t.Id,
                    FullName = t.FullName,
                    SubjectName = t.SubjectName,
                    BusinessPhoneNumber = t.BusinessPhoneNumber,
                    PersonalPhoneNumber = t.PersonalPhoneNumber,

                    CreatedDate = t.CreatedDate,
                    CreatedBy = t.CreatedBy,
                    ModifiedBy = t.ModifiedBy,
                    ModifiedDate = t.ModifiedDate,
                    Cancelled = t.Cancelled,
                    //TeacherStudents = t.TeacherStudents
                    //    .Where(ts => !ts.Student.Cancelled)
                    //    .Select(ts => new StudentsViewModel
                    //    {
                    //        Id = ts.Student.Id,
                    //        ArName = ts.Student.ArName,
                    //        EnName = ts.Student.EnName,
                    //        SerialNumber = ts.Student.SerialNumber,
                    //        UserName = ts.Student.UserName,
                    //        Password = ts.Student.Password,

                    //        CreatedDate = ts.Student.CreatedDate,
                    //        CreatedBy = ts.Student.CreatedBy,
                    //        ModifiedBy = ts.Student.ModifiedBy,
                    //        ModifiedDate = ts.Student.ModifiedDate
                    //    }).ToList(),
                    StudentsCount = t.TeacherStudents.Where(ts => !ts.Student.Cancelled).Count(),
                }).FirstOrDefaultAsync();

            return _teacher ?? new TeacherViewModel();
        }
    }
}
