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
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        IDatatableGridItemService _iDatatableGridItemService;

        public TeachersController(ApplicationDbContext context, IDatatableGridItemService IDatatableGridItemService)
        {
            _context = context;
            _iDatatableGridItemService = IDatatableGridItemService;
        }

        [Authorize(Roles = CodeYoDAL.DALHelpers.MainMenu.Teachers.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
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

                var _GetGridItem = _iDatatableGridItemService.GetTeachersGridItem();

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
                    || obj.FullName.ToLower().Contains(searchValue)
                    || obj.PersonalPhoneNumber.ToLower().Contains(searchValue)
                    || obj.BusinessPhoneNumber.ToLower().Contains(searchValue)
                    || obj.SubjectName.ToLower().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue)
                    || obj.ModifiedBy.ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().Contains(searchValue)
                    || obj.CreatedDate.ToString().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();

                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(string TeacherId)
        {
            TeacherViewModel vm = new TeacherViewModel();
            Guid _TeacherId;
            if (Guid.TryParse(TeacherId, out _TeacherId))
            {
                vm = await _iDatatableGridItemService.GetTeacherAsync(_TeacherId);
            }
            return PartialView("_AddEdit", vm);

        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(TeacherViewModel vm)
        {
            JsonResultViewModel _response = new();
            try
            {
                if (ModelState.IsValid)
                {
                    _response = await _iDatatableGridItemService.AddEditTeacherAsync(vm);
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
        public async Task<IActionResult> Details(string TeacherId)
        {
            TeacherViewModel vm = new TeacherViewModel();
            Guid _TeacherId;
            if (Guid.TryParse(TeacherId, out _TeacherId))
            {
                vm = await _iDatatableGridItemService.GetTeacherAsync(_TeacherId);
            }
            return PartialView("_Details", vm);
        }



        [HttpPost]
        public async Task<JsonResult> Delete(string TeacherId)
        {
            JsonResultViewModel _response = new();
            try
            {
                Guid _TeacherId;
                if (Guid.TryParse(TeacherId, out _TeacherId))
                {
                    _response = await _iDatatableGridItemService.DeleteTeacherAsync(_TeacherId);
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
