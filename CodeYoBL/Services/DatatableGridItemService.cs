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
using static CodeYoDAL.DALHelpers.MainMenu;

namespace CodeYoBL.Services
{
    public class DatatableGridItemService : IDatatableGridItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DatatableGridItemService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public IQueryable<TeacherViewModel> GetTeachersGridItem()
        {
            try
            {
                var query = from _Teacher in _context.Teachers
                            where !_Teacher.Cancelled
                            join ts in _context.TeacherStudents on _Teacher.Id equals ts.TeacherId into studentTeachers
                            select new TeacherViewModel
                            {
                                Id = _Teacher.Id,
                                FullName = _Teacher.FullName,
                                PersonalPhoneNumber = _Teacher.PersonalPhoneNumber,
                                BusinessPhoneNumber = _Teacher.BusinessPhoneNumber,
                                SubjectName = _Teacher.SubjectName,

                                CreatedDate = _Teacher.CreatedDate,
                                CreatedBy = _Teacher.CreatedBy,
                                ModifiedDate = _Teacher.ModifiedDate,
                                ModifiedBy = _Teacher.ModifiedBy,
                                StudentsCount = studentTeachers.Count()
                            };

                return query.OrderByDescending(x => x.CreatedDate);

                //return (from _Teacher in _context.Teachers
                //        where !_Teacher.Cancelled
                //        select new TeacherViewModel
                //        {
                //            Id = _Teacher.Id,
                //            FullName = _Teacher.FullName,
                //            PersonalPhoneNumber = _Teacher.PersonalPhoneNumber,
                //            BusinessPhoneNumber = _Teacher.BusinessPhoneNumber,
                //            SubjectName = _Teacher.SubjectName,

                //            CreatedDate = _Teacher.CreatedDate,
                //            CreatedBy = _Teacher.CreatedBy,
                //            ModifiedDate = _Teacher.ModifiedDate,
                //            ModifiedBy = _Teacher.ModifiedBy
                //        }).OrderByDescending(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<StudentsViewModel> GetStudentsGridItem()
        {
            try
            {
                var query = from student in _context.Students
                            where !student.Cancelled
                            join ts in _context.TeacherStudents on student.Id equals ts.StudentId into studentTeachers
                            select new StudentsViewModel
                            {
                                Id = student.Id,
                                ArName = student.ArName,
                                EnName = student.EnName,
                                UserName = student.UserName,
                                SerialNumber = student.SerialNumber,
                                Password = student.Password,
                                CreatedDate = student.CreatedDate,
                                CreatedBy = student.CreatedBy,
                                ModifiedDate = student.ModifiedDate,
                                ModifiedBy = student.ModifiedBy,
                                TeachersCount = studentTeachers.Count()
                            };

                return query.OrderByDescending(x => x.CreatedDate);


                //return (from _Student in _context.Students
                //        where !_Student.Cancelled
                //        select new StudentsViewModel
                //        {
                //            Id = _Student.Id,
                //            ArName = _Student.ArName,
                //            EnName = _Student.EnName,
                //            UserName = _Student.UserName,
                //            SerialNumber = _Student.SerialNumber,
                //            Password = _Student.Password,

                //            CreatedDate = _Student.CreatedDate,
                //            CreatedBy = _Student.CreatedBy,
                //            ModifiedDate = _Student.ModifiedDate,
                //            ModifiedBy = _Student.ModifiedBy
                //        }).OrderByDescending(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
