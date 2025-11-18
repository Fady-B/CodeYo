using CodeYoBL;
using CodeYoBL.IServices;
using CodeYoDAL.Data;
using CodeYoDAL.Models;
using CodeYoDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Configuration;
using NuGet.Packaging;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using static CodeYoDAL.DALHelpers.MainMenu;

namespace SchoolMS.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class CardsDesignController : Controller
    {
        private readonly ApplicationDbContext _context;
        IDatatableGridItemService _iDatatableGridItemService;
        IStudentService _iStudentService;
        ICommonService _iCommon;

        public CardsDesignController(ApplicationDbContext context, IDatatableGridItemService IDatatableGridItemService, IStudentService iStudentService, ICommonService ICommonService)
        {
            _context = context;
            _iDatatableGridItemService = IDatatableGridItemService;
            _iStudentService = iStudentService;
            _iCommon = ICommonService;
        }

        [Authorize(Roles = CodeYoDAL.DALHelpers.MainMenu.DesignCard.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.DdlTeachers = new SelectList(_iCommon.GetTeachersDdl(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetStudenData(string Name)
        {
            try
            {
                JsonResultViewModel result = new();
                if (string.IsNullOrEmpty(Name))
                {
                    result.IsSuccess = false;
                    result.AlertMessage = "Couldn't find what you are looking for!!";
                    return new JsonResult(result);
                }
                var Student = await _iStudentService.GetFirstSudentAsync() ?? null;
                if (Student == null)
                {
                    result.IsSuccess = false;
                    return new JsonResult(result);
                }
                switch (Name)
                {
                    case "SerialNumber":

                        result.IsSuccess = true;
                        result.ReturnData = Student.SerialNumber;
                        break;

                    case "Username":

                        result.IsSuccess = true;
                        result.ReturnData = Student.UserName;
                        break;

                    case "Password":

                        result.IsSuccess = true;
                        result.ReturnData = Student.Password;
                        break;

                    default:
                        result.IsSuccess = false;
                        result.AlertMessage = "Couldn't find what you are looking for!!";
                        break;
                }
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCardDesign( CardsDesignViewModel vm)
        {
            JsonResultViewModel _response = new();
            try
            {
                _response.AlertMessage = "Design Saved!";
                _response.IsSuccess = true;
                return new JsonResult(_response);
            }
            catch (DbUpdateConcurrencyException)
            {
                _response.IsSuccess = false;
                _response.AlertMessage = "Operation Failed!";
                return new JsonResult(_response);
                throw;
            }
        }


        //[HttpGet]
        //public async Task<IActionResult> AddEdit(string Id)
        //{
        //    try
        //    {
        //        StudentsViewModel vm = new StudentsViewModel();
        //        Guid _StudentId;
        //        if (Guid.TryParse(Id, out _StudentId))
        //        {
        //            vm = await _iStudentService.GetAsync(_StudentId);
        //        }
        //        ViewBag.DdlTeachers = new SelectList(_iCommon.GetTeachersDdl(), "Id", "Name");
        //        return PartialView("_AddEdit", vm);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddEdit(StudentsViewModel vm)
        //{
        //    JsonResultViewModel _response = new();
        //    try
        //    {
        //        if (vm.Id == Guid.Empty)
        //            ModelState.Remove("Id");

        //        if (ModelState.IsValid)
        //        {
        //            _response = await _iStudentService.AddEditAsync(vm);
        //        }
        //        else
        //        {
        //            _response.IsSuccess = false;
        //            _response.AlertMessage = "Please make sure to fill all the required fields!";
        //        }
        //        return new JsonResult(_response);
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.AlertMessage = "Operation Failed!";
        //        return new JsonResult(_response);
        //        throw ex;
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> Details(string Id)
        //{
        //    try
        //    {
        //        StudentsViewModel vm = new StudentsViewModel();
        //        Guid _StudentId;
        //        if (Guid.TryParse(Id, out _StudentId))
        //        {
        //            vm = await _iStudentService.GetAsync(_StudentId);
        //        }
        //        return PartialView("_Details", vm);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpPost]
        //public async Task<JsonResult> Delete(string Id)
        //{
        //    JsonResultViewModel _response = new();
        //    try
        //    {
        //        Guid _StudentId;
        //        if (Guid.TryParse(Id, out _StudentId))
        //        {
        //            _response = await _iStudentService.DeleteAsync(_StudentId);
        //        }
        //        else
        //        {
        //            _response.IsSuccess = false;
        //            _response.AlertMessage = "Operation Failed!";
        //        }
        //        return new JsonResult(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.AlertMessage = "Operation Failed!";
        //        return new JsonResult(_response);
        //        throw ex;
        //    }
        //}

    }
}
