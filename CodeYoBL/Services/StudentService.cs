using CodeYoBL.IServices;
using CodeYoDAL.Data;
using CodeYoDAL.Models;
using CodeYoDAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShaghalnyDAL.Models;

namespace CodeYoBL.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudentService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JsonResultViewModel> AddEditAsync(StudentsViewModel vm)
        {
            JsonResultViewModel _Result = new();
            try
            {
                Students _Student = new();
                string _UserName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                if (vm.Id != Guid.Empty) //Edit
                {

                    _Student = await _context.Students
                        .Include(s => s.TeacherStudents)
                        .FirstOrDefaultAsync(s => s.Id == vm.Id);

                    if (_Student == null)
                    {
                        _Result.IsSuccess = false;
                        _Result.AlertMessage = "Operation Failed: Couldn't find the student!";
                        return _Result;
                    }

                    vm.CreatedDate = _Student.CreatedDate;
                    vm.CreatedBy = _Student.CreatedBy;
                    vm.ModifiedDate = DateTime.Now;
                    vm.ModifiedBy = _UserName;
                    _context.Entry(_Student).CurrentValues.SetValues(vm);

                    if (_Student.TeacherStudents != null && _Student.TeacherStudents.Any())
                    {
                        _context.TeacherStudents.RemoveRange(_Student.TeacherStudents);
                        _Student.TeacherStudents = new List<TeacherStudents>();
                    }
                        

                    if (vm.TeachersIds != null && vm.TeachersIds.Any())
                    {
                        _Student.TeacherStudents ??= new List<TeacherStudents>();
                        foreach (var teacherId in vm.TeachersIds)
                        {
                            _Student.TeacherStudents.Add(new TeacherStudents
                            {
                                TeacherId = teacherId,
                                StudentId = _Student.Id
                            });
                        }
                    }

                    await _context.SaveChangesAsync();

                    _Result.AlertMessage = $"Student updated successfully with the name of: {_Student.EnName}.";

                    //_Student = await _context.Students.FindAsync(vm.Id);
                    //if (_Student == null)
                    //{
                    //    _Result.IsSuccess = false;
                    //    _Result.AlertMessage = "Operation Failed: Couldn't find the student!";
                    //    return _Result;
                    //}
                    //vm.CreatedDate = _Student.CreatedDate;
                    //vm.CreatedBy = _Student.CreatedBy;
                    //vm.ModifiedDate = DateTime.Now;
                    //vm.ModifiedBy = _UserName;

                    //_context.Entry(_Student).CurrentValues.SetValues(vm);
                    //await _context.SaveChangesAsync();

                    //_Result.AlertMessage = $"Student updated successfully with the name of: {_Student.EnName}.";
                }
                else //Add
                {
                    vm.Id = Guid.NewGuid();
                    _Student = vm;
                    _Student.CreatedDate = DateTime.Now;
                    _Student.ModifiedDate = DateTime.Now;
                    _Student.CreatedBy = _UserName;
                    _Student.ModifiedBy = _UserName;
                    if (vm.TeachersIds != null && vm.TeachersIds.Any())
                    {
                        _Student.TeacherStudents ??= new List<TeacherStudents>();
                        foreach (var teacherId in vm.TeachersIds)
                        {
                            _Student.TeacherStudents.Add(new TeacherStudents
                            {
                                TeacherId = teacherId,
                                StudentId = _Student.Id
                            });
                        }
                    }


                    _context.Add(_Student);
                    await _context.SaveChangesAsync();

                    _Result.AlertMessage = $"Student has been added successfully with the name of: {_Student.EnName}.";
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
                var _Student = await _context.Students.FindAsync(Id);

                if (_Student == null)
                {
                    _Result.IsSuccess = false;
                    _Result.AlertMessage = "Operation Failed!";
                    return _Result;
                }
                _Result.AlertMessage = $"The student: '{_Student.EnName}' has been deleted successfully.";

                var _TeacherStudents = _context.TeacherStudents.Where(x => x.StudentId == Id);
                _context.TeacherStudents.RemoveRange(_TeacherStudents);

                _context.Remove(_Student);
                await _context.SaveChangesAsync();

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

        public async Task<StudentsViewModel> GetAsync(Guid Id)
        {
            var student = await _context.Students
                .Include(s => s.TeacherStudents)
                .ThenInclude(ts => ts.Teacher)
                .Where(s => s.Id == Id && !s.Cancelled)
                .Select(s => new StudentsViewModel
                {
                    Id = s.Id,
                    ArName = s.ArName,
                    EnName = s.EnName,
                    SerialNumber = s.SerialNumber,
                    UserName = s.UserName,
                    Password = s.Password,
                    CreatedDate = DateTime.Now,
                    CreatedBy = s.CreatedBy,
                    ModifiedBy = s.ModifiedBy,
                    ModifiedDate = DateTime.Now,
                    Cancelled = s.Cancelled,
                    StudentTeachers = s.TeacherStudents
                        .Where(ts => !ts.Teacher.Cancelled)
                        .Select(ts => new TeacherViewModel
                        {
                            Id = ts.Teacher.Id,
                            FullName = ts.Teacher.FullName,
                            BusinessPhoneNumber = ts.Teacher.BusinessPhoneNumber,
                            PersonalPhoneNumber = ts.Teacher.PersonalPhoneNumber,
                            SubjectName = ts.Teacher.SubjectName,
                            CreatedDate = ts.Teacher.CreatedDate,
                            CreatedBy = ts.Teacher.CreatedBy,
                            ModifiedBy = ts.Teacher.ModifiedBy,
                            ModifiedDate = ts.Teacher.ModifiedDate
                        }).ToList(),
                    TeachersIds = s.TeacherStudents.Select(id => id.TeacherId).ToList(),
                }).FirstOrDefaultAsync();

            return student ?? new StudentsViewModel();

            //return await _context.Students.Where(t => t.Id == Id && !t.Cancelled).FirstOrDefaultAsync() ?? new StudentsViewModel();
        }
    }
}
