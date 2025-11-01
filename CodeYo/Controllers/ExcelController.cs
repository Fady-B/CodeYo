using CodeYoBL.IServices;
using CodeYoDAL.DALHelpers;
using CodeYoDAL.Data;
using CodeYoDAL.Models;
using CodeYoDAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using ShaghalnyDAL.Models;
using System.Data;
using System.Drawing;

namespace SchoolMS.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class ExcelController : Controller
    {
        private readonly ApplicationDbContext _context;
        //IDatatableGridItemService _iDatatableGridItemService;
        IStudentService _iStudentService;
        ICommonService _iCommon;

        private readonly ExcelFileHelper _excelFileHelper = new();

        public ExcelController(ApplicationDbContext context, /*IDatatableGridItemService IDatatableGridItemService,*/ IStudentService iStudentService, ICommonService ICommonService)
        {
            _context = context;
            //_iDatatableGridItemService = IDatatableGridItemService;
            _iStudentService = iStudentService;
            _iCommon = ICommonService;
        }

        //[Authorize(Roles = CodeYoDAL.DALHelpers.MainMenu.Students.RoleName)]
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult GetDataTabelData()
        //{
        //    try
        //    {
        //        var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();

        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //        var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int resultTotal = 0;

        //        var _GetGridItem = _iDatatableGridItemService.GetStudentsGridItem();

        //        //Sorting
        //        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
        //        {
        //            _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
        //        }

        //        //Search
        //        if (!string.IsNullOrEmpty(searchValue))
        //        {
        //            searchValue = searchValue.ToLower();
        //            _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
        //            || obj.ArName.ToLower().Contains(searchValue)
        //            || obj.EnName.ToLower().Contains(searchValue)
        //            || obj.SerialNumber.ToLower().Contains(searchValue)
        //            || obj.UserName.ToLower().Contains(searchValue)
        //            || obj.Password.ToLower().Contains(searchValue)
        //            || obj.TeachersCount.ToString().ToLower().Contains(searchValue)
        //            || obj.CreatedBy.ToLower().Contains(searchValue)
        //            || obj.ModifiedBy.ToLower().Contains(searchValue)
        //            || obj.ModifiedDate.ToString().Contains(searchValue)
        //            || obj.CreatedDate.ToString().Contains(searchValue));
        //        }

        //        resultTotal = _GetGridItem.Count();

        //        var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
        //        return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        [HttpGet]
        public async Task<JsonResult> CreateStudntExcelSheet()
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("StudentsDataSheet");
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Student Arabic Name", typeof(string));
                    dataTable.Columns.Add("Student English Name", typeof(string));
                    dataTable.Columns.Add("Student Serial Number", typeof(string));
                    dataTable.Columns.Add("Student Username", typeof(string));
                    dataTable.Columns.Add("Student Password", typeof(string));

                    //Requerd Fileds
                    #region
                    var StudentSerialNumberCell = worksheet.Cells["C1"];

                    var fillColor = Color.Red;
                    StudentSerialNumberCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    StudentSerialNumberCell.Style.Fill.BackgroundColor.SetColor(fillColor);

                    var StudentSerialNumber = worksheet.Cells["C:C"];
                    var StudentSerialNumberValidation = StudentSerialNumber.DataValidation.AddListDataValidation();
                    StudentSerialNumberValidation.ShowInputMessage = true;
                    StudentSerialNumberValidation.PromptTitle = "CodeYo";
                    StudentSerialNumberValidation.Prompt = "Required";

                    #endregion
                    //Freeze
                    worksheet.View.FreezePanes(1, 1);
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, PrintHeaders: true);

                    int lastRow = ExcelPackage.MaxRows;
                    var table = worksheet.Tables.Add(worksheet.Cells[$"A1:E{lastRow}"], "StudentsDataTable");
                    var columnsToLock = new List<int> { 1, 2 };

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        foreach (var col in columnsToLock)
                        {
                            var cell = worksheet.Cells[row, col];
                            cell.Style.Locked = true;
                        }
                    }


                    worksheet.Cells.AutoFitColumns();
                    var startCell = worksheet.Cells[1, 1];
                    var endCell = worksheet.Cells[worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
                    var dataRange = worksheet.Cells[startCell.Address + ":" + endCell.Address];
                    dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Protection.AllowAutoFilter = false;
                    worksheet.Protection.SetPassword("a8dAC60a");
                    worksheet.Protection.AllowSelectLockedCells = true;


                    table.TableStyle = TableStyles.Medium2;
                    var stream = new System.IO.MemoryStream();
                    package.SaveAs(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);

                    ExcelDownloadViewModel vm = new();
                    vm.FileName = "Code-Yo Student Data Tmplate.xlsx";
                    vm.DocByte = stream.ToArray();

                    return new JsonResult(vm);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //[HttpGet]
        //public async Task<JsonResult> DownloadStusdentsExcelSheet()
        //{
        //    try
        //    {
        //        ExcelSheetViewModel vm = new();
        //        string _WebRootPath = "wwwroot/Assets/Students Codes.xlsx";
        //        using (var _MemoryStream = new MemoryStream())
        //        {
        //            using (FileStream file = new FileStream(_WebRootPath, FileMode.Open, FileAccess.Read))
        //                file.CopyTo(_MemoryStream);
        //            vm.DocByte = _MemoryStream.ToArray();
        //        }

        //        vm.FileName = "Students Codes.xlsx";
        //        return new JsonResult(vm);
        //    }
        //    catch (Exception) { throw; }
        //}

        [HttpGet]
        public IActionResult UploadExcelPartial()
        {
            try
            {
                ViewBag.DdlTeachers = new SelectList(_iCommon.GetTeachersDdl(), "Id", "Name");
                return PartialView("_UploadExcelPartial");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadStudentsData(ExcelFileDTOViewModel vm)
        {
            JsonResultViewModel _JsonResultViewModel = new();
            List<string> validationMessages = new();
            try
            {
                var excelDataSet = await _excelFileHelper.ReadExcelFile(vm.ExcelFileInfo);

                if (excelDataSet == null)
                {
                    _JsonResultViewModel.AlertMessage = "Please choose a file!!";
                    _JsonResultViewModel.IsSuccess = false;
                    return new JsonResult(_JsonResultViewModel);
                }

                if (vm.TeachersIds == null || !vm.TeachersIds.Any())
                {
                    _JsonResultViewModel.AlertMessage = "Please select at least one teacher!";
                    _JsonResultViewModel.IsSuccess = false;
                    return new JsonResult(_JsonResultViewModel);
                }

                var StudentsData = excelDataSet.Tables[0].AsEnumerable();
                List<StudentsViewModel> StudentExcelList = new List<StudentsViewModel>();
                foreach (var item in StudentsData)
                {
                    StudentsViewModel StudentExcel = new StudentsViewModel();
                    StudentExcel.ArName = item.ItemArray[0].ToString();
                    StudentExcel.EnName = item.ItemArray[1].ToString();
                    StudentExcel.SerialNumber = item.ItemArray[2].ToString();
                    StudentExcel.UserName = item.ItemArray[3].ToString();
                    StudentExcel.Password = item.ItemArray[4].ToString();

                    StudentExcelList.Add(StudentExcel);
                }

                if (StudentExcelList.Count() <= 1)
                {
                    _JsonResultViewModel.AlertMessage = "Please add some data in the sheet before uploading!!";
                    _JsonResultViewModel.IsSuccess = false;
                    return new JsonResult(_JsonResultViewModel);
                }
                else
                {
                    // Remove header
                    StudentExcelList.RemoveAt(0);
                    validationMessages = await ValidateStudentExcelSheet(StudentExcelList);
                }

                if (validationMessages.Any())
                {
                    _JsonResultViewModel.AlertMessage = string.Join("<br />", validationMessages);
                    _JsonResultViewModel.IsSuccess = false;
                    return new JsonResult(_JsonResultViewModel);
                }

                _JsonResultViewModel = await AddEditStudentFromExcelSheet(StudentExcelList, vm.TeachersIds);

                _JsonResultViewModel.IsSuccess = true;
                return new JsonResult(_JsonResultViewModel);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _JsonResultViewModel.IsSuccess = false;
                _JsonResultViewModel.AlertMessage = "Operation Failed!";
                return new JsonResult(_JsonResultViewModel);
                throw ex;
            }
        }

        private async Task<List<string>> ValidateStudentExcelSheet(List<StudentsViewModel> studentsList)
        {
            List<string> validationMessages = new();
            if (studentsList.Count > 5000)
            {
                validationMessages.Add("The Maximum Amount To Upload Is 5000 Student...");
                return validationMessages;
            }

            var Students = _context.Students.Where(i => !i.Cancelled);

            int studentRow = 2;
            foreach (var item in studentsList)
            {
                var IfRepeated = studentsList.Where(i => i.SerialNumber == item.SerialNumber).ToList();
                if (IfRepeated.Count > 1)
                {
                    validationMessages.Add($"....Please be informed that Student Serial Number: '{item.SerialNumber}' is repeated in the excel file");
                }

                if (string.IsNullOrWhiteSpace(item.SerialNumber))
                    validationMessages.Add($"....Please add Student Serial Number in the row number: {studentRow}....");

                if (string.IsNullOrWhiteSpace(item.UserName))
                    validationMessages.Add($" ....Please add Student UserName in the row number: {studentRow}....");

                if (string.IsNullOrWhiteSpace(item.Password))
                    validationMessages.Add($" ....Please add Student Password in the row number: {studentRow}....");

                var IfExist = await Students.Where(i => i.SerialNumber == item.SerialNumber).AnyAsync();
                if (IfExist)
                {
                    validationMessages.Add($" ....Please be informed that Student: ' {item.EnName} ' in the row number: {studentRow} - already added before in the database....");
                }
                studentRow++;
            }

            return validationMessages;
        }

        private async Task<JsonResultViewModel> AddEditStudentFromExcelSheet(List<StudentsViewModel> StudentsList, List<Guid> TeachersIds)
        {
            try
            {
                JsonResultViewModel _JsonResultViewModel = new();
                var AllPastStudents = _context.Students.Where(i => !i.Cancelled);
                string UserName = HttpContext.User.Identity.Name;
                List<Students> StudentsToBeAdded = new List<Students>();
                List<Students> StudentsToBeUpdated = new List<Students>();
                foreach (var item in StudentsList)
                {
                    var IfExist = await AllPastStudents.FirstOrDefaultAsync(i => i.SerialNumber == item.SerialNumber);
                    if (IfExist == null)
                    {
                        try
                        {
                            // Set student data
                            Students student = new Students
                            {
                                Id = Guid.NewGuid(),
                                ArName = item.ArName,
                                EnName = item.EnName,
                                SerialNumber = item.SerialNumber,
                                UserName = item.UserName,
                                Password = item.Password,

                                CreatedBy = UserName,
                                ModifiedBy = UserName,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                            };

                            student.TeacherStudents = TeachersIds.Select(tId => new TeacherStudents
                            {
                                TeacherId = tId,
                                StudentId = student.Id
                            }).ToList();

                            StudentsToBeAdded.Add(student);

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        item.Id = IfExist.Id;
                        item.ModifiedBy = UserName;
                        item.ModifiedDate = DateTime.Now;
                        StudentsToBeUpdated.Add(item);
                    }
                }
                //List<Students> AllStudents = new List<Students>();
                if (StudentsToBeAdded.Count > 0)
                {
                    _context.AddRange(StudentsToBeAdded);
                    await _context.SaveChangesAsync();


                    //AllStudents.AddRange(StudentsToBeAdded);
                }

                if (StudentsToBeUpdated.Count > 0)
                {
                    _context.UpdateRange(StudentsToBeUpdated);
                    await _context.SaveChangesAsync();


                    //AllStudents.AddRange(StudentsToBeAdded);
                }

                //_JsonResultViewModel.PdfDTO = await GeneratePdfAfterUpload(AllStudents);

                _JsonResultViewModel.AlertMessage = "Students data uploaded successfully.";
                _JsonResultViewModel.IsSuccess = true;
                return _JsonResultViewModel;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }


    }
}
