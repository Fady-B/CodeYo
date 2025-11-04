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
using System.Text.RegularExpressions;

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

        public ExcelController(ApplicationDbContext context, IStudentService iStudentService, ICommonService ICommonService)
        {
            _context = context;
            _iStudentService = iStudentService;
            _iCommon = ICommonService;
        }

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
                    StudentExcel.SerialNumber = Regex.Replace(item.ItemArray[2].ToString(), @"\s+", "");
                    StudentExcel.UserName = Regex.Replace(item.ItemArray[3].ToString(), @"\s+", "");
                    StudentExcel.Password = Regex.Replace(item.ItemArray[4].ToString(), @"\s+", "");

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
                //List<Students> StudentsToBeUpdated = new List<Students>();
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
                    //else
                    //{
                    //    item.Id = IfExist.Id;
                    //    item.ModifiedBy = UserName;
                    //    item.ModifiedDate = DateTime.Now;
                    //    StudentsToBeUpdated.Add(item);
                    //}
                }
                //List<Students> AllStudents = new List<Students>();
                if (StudentsToBeAdded.Count > 0)
                {
                    _context.AddRange(StudentsToBeAdded);
                    await _context.SaveChangesAsync();


                    //AllStudents.AddRange(StudentsToBeAdded);
                }

                //if (StudentsToBeUpdated.Count > 0)
                //{
                //    _context.UpdateRange(StudentsToBeUpdated);
                //    await _context.SaveChangesAsync();


                //    //AllStudents.AddRange(StudentsToBeAdded);
                //}

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
