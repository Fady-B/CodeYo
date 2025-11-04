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

        [HttpPost]
        public IActionResult GetDataTabelData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var _GetGridItem = _iDatatableGridItemService.GetStudentsGridItem();

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
                    || obj.ArName.ToLower().Contains(searchValue)
                    || obj.EnName.ToLower().Contains(searchValue)
                    || obj.SerialNumber.ToLower().Contains(searchValue)
                    || obj.UserName.ToLower().Contains(searchValue)
                    || obj.Password.ToLower().Contains(searchValue)
                    || obj.TeachersCount.ToString().ToLower().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue)
                    || obj.ModifiedBy.ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().Contains(searchValue)
                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(string Id)
        {
            try
            {
                StudentsViewModel vm = new StudentsViewModel();
                Guid _StudentId;
                if (Guid.TryParse(Id, out _StudentId))
                {
                    vm = await _iStudentService.GetAsync(_StudentId);
                }
                ViewBag.DdlTeachers = new SelectList(_iCommon.GetTeachersDdl(), "Id", "Name");
                return PartialView("_AddEdit", vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(StudentsViewModel vm)
        {
            JsonResultViewModel _response = new();
            try
            {
                if (vm.Id == Guid.Empty)
                    ModelState.Remove("Id");

                if (ModelState.IsValid)
                {
                    _response = await _iStudentService.AddEditAsync(vm);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.AlertMessage = "Please make sure to fill all the required fields!";
                }
                return new JsonResult(_response);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _response.IsSuccess = false;
                _response.AlertMessage = "Operation Failed!";
                return new JsonResult(_response);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            try
            {
                StudentsViewModel vm = new StudentsViewModel();
                Guid _StudentId;
                if (Guid.TryParse(Id, out _StudentId))
                {
                    vm = await _iStudentService.GetAsync(_StudentId);
                }
                return PartialView("_Details", vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string Id)
        {
            JsonResultViewModel _response = new();
            try
            {
                Guid _StudentId;
                if (Guid.TryParse(Id, out _StudentId))
                {
                    _response = await _iStudentService.DeleteAsync(_StudentId);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.AlertMessage = "Operation Failed!";
                }
                return new JsonResult(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.AlertMessage = "Operation Failed!";
                return new JsonResult(_response);
                throw ex;
            }
        }

    }
}
