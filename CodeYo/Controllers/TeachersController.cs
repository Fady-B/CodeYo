using CodeYoDAL.Data;
using CodeYoDAL.Models;
using CodeYoDAL.ViewModels;
//using CodeYoBL;
//using CodeYoBL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Collections.Generic;
using NuGet.Packaging;
using System.Linq;
using System.Text.RegularExpressions;

namespace SchoolMS.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = CodeYoDAL.DALHelpers.MainMenu.Teachers.RoleName)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult GetDataTabelData(Int64 TeacherRole, Int64 SubjectId, string lang)
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

        //        var _GetGridItem = GetGridItem(lang);

        //        if (TeacherRole > 0)
        //        {
        //            _GetGridItem = _GetGridItem.Where(t => t.TeacherRoleId == TeacherRole).AsQueryable();
        //        }

        //        if (SubjectId > 0)
        //        {
        //            List<TeacherCRUDViewModel> teachers = new List<TeacherCRUDViewModel>();
        //            foreach (var GridItem in _GetGridItem)
        //            {
        //                if (GridItem.TeacherRoleId == 1) //Teacher
        //                {
        //                    var TeachersSubjectsRelation = _context.TeacherSubjects.Where(s => s.SubjectId == SubjectId && s.TeacherId == GridItem.Id).ToList();
        //                    foreach (var relation in TeachersSubjectsRelation)
        //                    {
        //                        var Teacher = _context.Teacher.Where(t => t.Id == relation.TeacherId && t.Cancelled == false).FirstOrDefault();
        //                        if (Teacher != null)
        //                        {
        //                            teachers.Add(Teacher);
        //                        }

        //                    }
        //                }
        //                else if (GridItem.TeacherRoleId == 2)//HeadMaster
        //                {
        //                    //No Subjects For HeadMaster
        //                }
        //                else //Supervisor
        //                {
        //                    if (GridItem.SubjectId == SubjectId)
        //                    {
        //                        teachers.Add(GridItem);
        //                    }
        //                }
        //            }
        //            _GetGridItem = teachers.AsQueryable();
        //        }

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
        //            || obj.Name.ToLower().Contains(searchValue)
        //            || obj.Email.ToLower().Contains(searchValue)
        //            || obj.PhoneNumber.ToLower().Contains(searchValue)
        //            || obj.Gender.ToLower().Contains(searchValue)
        //            || obj.BloodGroup.ToLower().Contains(searchValue)
        //            || obj.Address.ToLower().Contains(searchValue)

        //            || obj.CreatedDate.ToString().Contains(searchValue));
        //        }

        //        resultTotal = _GetGridItem.Count();

        //        var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
        //        return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //[HttpGet]
        //public async Task<IActionResult> Details(Int64 id)
        //{
        //    TeacherCRUDViewModel vm = await GetByTeacher(id).SingleOrDefaultAsync();
        //    if (vm == null) return NotFound();
        //    return PartialView("_Details", vm);
        //}
        //[HttpGet]
        //public async Task<ActionResult> GetSectionBySession(Int64 id)
        //{
        //    var v = _iCommon.GetddlSectionBySessionId(id);
        //    ViewBag.AllSection = new SelectList(_iCommon.GetddlSectionBySessionId(id), "Id", "Name");
        //    return Json(_iCommon.GetddlSectionBySessionId(id));
        //}

        //[HttpGet]
        //public async Task<IActionResult> AddEdit(int id)
        //{
        //    ViewBag.LoadddlDesignation = new SelectList(_iCommon.LoadddlDesignation(), "Id", "Name");
        //    ViewBag.LoadddlTeacherRole = new SelectList(_iCommon.LoadddTeacherRole(), "Id", "Name");
        //    ViewBag.AllSubject = new SelectList(_iCommon.GetddlSubject(), "Id", "Name");
        //    ViewBag.ddlSession = new SelectList(_context.Session.ToList(), "Id", "Name");
        //    ViewBag.AllEducationalLevelsMultible = new SelectList(_iCommon.GetddlEducationLevels(), "Id", "Name");


        //    TeacherCRUDViewModel vm = new TeacherCRUDViewModel();
        //    if (id > 0)
        //    {
        //        var teacherContext = await _context.Teacher.Where(x => x.Id == id && x.Cancelled == false).SingleOrDefaultAsync();

        //        if (teacherContext == null)
        //        {
        //            return NotFound();
        //        }


        //        if (teacherContext.SessionId == null)
        //        {
        //            teacherContext.SessionId = 0;
        //        }
        //        if (teacherContext.EducationLevelId == null)
        //        {
        //            teacherContext.EducationLevelId = 0;
        //        }
        //        if (teacherContext.SectionId == null)
        //        {
        //            teacherContext.SectionId = 0;
        //        }

        //        vm = teacherContext;
        //        vm.TeacherRoleIdDisplay = _context.TeacherRole.Where(t => t.Id == vm.TeacherRoleId).FirstOrDefault().TeacherRoleName;
        //        vm.DesignationDisplay = _context.Designation.Where(t => t.Id == vm.DesignationId).FirstOrDefault().Name;

        //        vm.ClassesIdList = new List<long>();
        //        vm.SubjectsIdList = new List<long>();


        //        var ClassesList = await _context.ClassInfoTeacher.Where(I => I.TeachersId == vm.Id).ToListAsync();
        //        if (ClassesList.Count > 0)
        //        {
        //            vm.ClassesIdList.AddRange(ClassesList.Select(i => i.ClassesId));
        //        }


        //        var SubjectsList = await _context.TeacherSubjects.Where(I => I.TeacherId == vm.Id).ToListAsync();
        //        if (SubjectsList.Count > 0)
        //        {
        //            vm.SubjectsIdList.AddRange(SubjectsList.Select(i => i.SubjectId));
        //        }

        //        var AllClasses = await _context.ClassInfo.Where(i => !i.Cancelled).ToListAsync();
        //        var SelectedClasses = AllClasses.Where(i => vm.ClassesIdList.Contains(i.Id)).ToList();

        //        List<Int64> SectionIds = new List<Int64>();

        //        SelectedClasses.ForEach(i =>
        //        {
        //            if (!SectionIds.Contains(i.SectionId))
        //            {
        //                SectionIds.Add(i.SectionId);
        //            }
        //        });

        //        List<ItemDropdownListViewModel> ItemDropdownList = (from classInfo in AllClasses
        //                                                            where !classInfo.Cancelled && SectionIds.Contains(classInfo.SectionId)
        //                                                            select new ItemDropdownListViewModel()
        //                                                            {
        //                                                                Id = classInfo.Id,
        //                                                                Name = classInfo.Name,
        //                                                            }).OrderBy(i => i.Id).ToList();

        //        ViewBag.AllClass = new SelectList(ItemDropdownList, "Id", "Name");

        //        ViewBag.GetddlSection = new SelectList(GetddlSection(vm.EducationLevelId), "Id", "Name");
        //        ViewBag.AllEducationalLevels = new SelectList(GetddlEducationLevels(vm.SessionId), "Id", "Name");
        //        return PartialView("_Edit", vm);
        //    }
        //    else
        //    {
        //        return PartialView("_Add", vm);
        //    }
        //}

        //[HttpGet]
        //public ItemDropdownListViewModel GetddlClassforTeacher(Int64 ClassId)
        //{
        //    return (from tblObj in _context.ClassInfo.Where(x => x.Cancelled == false && x.Id == ClassId)
        //            select new ItemDropdownListViewModel
        //            {
        //                Id = tblObj.Id,
        //                Name = tblObj.Name
        //            }).FirstOrDefault();
        //}

        //[HttpGet]
        //public IQueryable<ItemDropdownListViewModel> GetClassesWithSectionId(Int64? SectionId)
        //{
        //    return (from tblObj in _context.ClassInfo.Where(x => x.Cancelled == false)
        //            where tblObj.SectionId == SectionId
        //            select new ItemDropdownListViewModel
        //            {
        //                Id = tblObj.Id,
        //                Name = tblObj.Name
        //            }).OrderByDescending(x => x.Id);
        //}
        //[HttpGet]
        //public IQueryable<ItemDropdownListViewModel> GetddlEducationLevels(Int64? SessionId)
        //{
        //    return (from tblObj in _context.EducationalLevels
        //            where tblObj.SessionId == SessionId
        //            select new ItemDropdownListViewModel
        //            {
        //                Id = tblObj.Id,
        //                Name = tblObj.Name
        //            }).OrderBy(x => x.Id);
        //}
        //[HttpGet]
        //public IQueryable<ItemDropdownListViewModel> GetddlSection(Int64? EducationalLevelId)
        //{
        //    return (from tblObj in _context.Section
        //            where tblObj.EducationLevelId == EducationalLevelId
        //            select new ItemDropdownListViewModel
        //            {
        //                Id = tblObj.Id,
        //                Name = tblObj.Name
        //            }).OrderByDescending(x => x.Id);
        //}


        //[HttpPost]
        //public async Task<IActionResult> AddEdit(TeacherCRUDViewModel vm)
        //{
        //    try
        //    {
        //        JsonResultViewModel _JsonResultViewModel = new();
        //        string _UserName = HttpContext.User.Identity.Name;
        //        Teacher _Teacher = new Teacher();
        //        if (vm.Id > 0)
        //        {
        //            _Teacher = await _context.Teacher.FindAsync(vm.Id);

        //            if(_Teacher.ProfileImageId== Guid.Empty)
        //            {
        //                vm.ProfileImageId = _iCommon.UploadedStaticFile(StaticData.UserProfileAttachments);

        //            }
        //            if (vm.ProfileImageInfo != null)
        //            {
        //                vm.ProfileImageId =  _iCommon.UploadedFile(vm.ProfileImageInfo,StaticData.UserProfileAttachments);
        //            }
             

        //            if (vm.SessionId == null)
        //            {
        //                _Teacher.SessionId = 0;
        //                vm.SessionId = 0;
        //            }
        //            vm.CreatedDate = _Teacher.CreatedDate;
        //            vm.CreatedBy = _Teacher.CreatedBy;
        //            vm.ModifiedDate = DateTime.Now;
        //            vm.ModifiedBy = _UserName;

        //            _context.Entry(_Teacher).CurrentValues.SetValues(vm);
        //            await _context.SaveChangesAsync();

        //            //var result = await _iAccount.UpdateCommonUserProfile(vm);

        //            var AllSubjectsJson = JsonConvert.DeserializeObject<ICollection<Subject>>(vm.SubjectsText);
        //            var OLdSubjects = await _context.TeacherSubjects.Where(TS => TS.TeacherId == vm.Id).ToListAsync();
        //            foreach (var OLdSubject in OLdSubjects) //Old Subjects
        //            {
        //                _context.TeacherSubjects.Remove(OLdSubject);
        //            }
        //            List<TeacherSubjects> teacherSubjectslist = new List<TeacherSubjects>();
        //            foreach (var NewSubject in AllSubjectsJson) //New Subjects
        //            {
        //                TeacherSubjects teacherSubjects = new TeacherSubjects();
        //                teacherSubjects.TeacherId = _Teacher.Id;
        //                teacherSubjects.SubjectId = NewSubject.Id;
        //                teacherSubjectslist.Add(teacherSubjects);
        //            }
        //            _context.AddRange(teacherSubjectslist);
        //            await _context.SaveChangesAsync();


        //            var allClasses = JsonConvert.DeserializeObject<ICollection<DigitalSchoolDAL.Models.ClassInfo>>(vm.ClassesText);
        //            var allClassInfo = new List<ClassInfoTeacher>();
        //            var t = await _context.ClassInfoTeacher.Where(x => x.TeachersId == vm.Id).ToListAsync();
        //            foreach (var old in t)//remove
        //            {
        //                _context.ClassInfoTeacher.Remove(old);
        //            }

        //            foreach (var one in allClasses) //add
        //            {
        //                var newRelation = new ClassInfoTeacher();
        //                newRelation.ClassesId = one.Id;
        //                newRelation.TeachersId = _Teacher.Id;

        //                _context.ClassInfoTeacher.Add(newRelation);
        //                await _context.SaveChangesAsync();
        //            }

        //            _JsonResultViewModel.AlertMessage = "Teacher Updated Successfully. Name: " + _Teacher.Name;
        //            _JsonResultViewModel.IsSuccess = true;
        //            return new JsonResult(_JsonResultViewModel);
        //        }

        //        #region
        //        //else
        //        //{
        //        //    UserProfileViewModel _UserProfileViewModel = vm;
        //        //    _UserProfileViewModel.Type = AppUserType.Teacher;
        //        //    var _ApplicationUser = await _iAccount.CreateApplicationUser(_UserProfileViewModel);

        //        //    if (_ApplicationUser.Item2 == "Success")
        //        //    {
        //        //        vm.ApplicationUserId = _ApplicationUser.Item1.Id;
        //        //        if (vm.ProfileImageInfo != null)
        //        //        {
        //        //            vm.ProfileImage = "/upload/" + _iCommon.UploadedFile(vm.ProfileImageInfo);
        //        //        }

        //        //        _UserProfileViewModel.ProfilePicture = vm.ProfileImage;
        //        //        _UserProfileViewModel.CreatedBy = _UserName;
        //        //        _UserProfileViewModel.ModifiedBy = _UserName;
        //        //        await _iAccount.AddCommonUserProfile(_UserProfileViewModel, _ApplicationUser);

        //        //        _Teacher = vm;
        //        //        _Teacher.CreatedDate = DateTime.Now;
        //        //        _Teacher.ModifiedDate = DateTime.Now;
        //        //        _Teacher.CreatedBy = _UserName;
        //        //        _Teacher.ModifiedBy = _UserName;
        //        //        _context.Add(_Teacher);

        //        //        await _context.SaveChangesAsync();


        //        //        var allEducationLevels = JsonConvert.DeserializeObject<ICollection<EducationalLevels>>(vm.EducationLevelsText);
        //        //        var AllEduInfoHeadMastr = new List<EducationLevelInfoHeadMaster>();
        //        //        foreach (var AddRelation in allEducationLevels)
        //        //        {
        //        //            EducationLevelInfoHeadMaster relation = new EducationLevelInfoHeadMaster();
        //        //            relation.EducationLevelId = AddRelation.Id;
        //        //            relation.HeadMasterId = _Teacher.Id;
        //        //            AllEduInfoHeadMastr.Add(relation);
        //        //        }
        //        //        _context.AddRange(AllEduInfoHeadMastr);
        //        //        await _context.SaveChangesAsync();

        //        //        var AllSubjectsJson = JsonConvert.DeserializeObject<ICollection<Subject>>(vm.SubjectsText);
        //        //        var TeacherSubjectsList = new List<TeacherSubjects>();
        //        //        foreach (var NewSubject in AllSubjectsJson)
        //        //        {
        //        //            TeacherSubjects teacherSubjects = new TeacherSubjects();
        //        //            teacherSubjects.TeacherId = _Teacher.Id;
        //        //            teacherSubjects.SubjectId = NewSubject.Id;
        //        //            TeacherSubjectsList.Add(teacherSubjects);
        //        //        }
        //        //        _context.AddRange(TeacherSubjectsList);
        //        //        await _context.SaveChangesAsync();


        //        //        var allClasses = JsonConvert.DeserializeObject<ICollection<ClassInfo>>(vm.ClassesText);
        //        //        var allClassInfo = new List<ClassInfoTeacher>();
        //        //        foreach (var one in allClasses)
        //        //        {
        //        //            var b = new ClassInfoTeacher();
        //        //            b.ClassesId = one.Id;
        //        //            b.TeachersId = _Teacher.Id;

        //        //            allClassInfo.Add(b);
        //        //        }
        //        //        _context.AddRange(allClassInfo);

        //        //        await _context.SaveChangesAsync();
        //        //        _JsonResultViewModel.AlertMessage = "Teacher Created Successfully. Name: " + _Teacher.Name;
        //        //        _JsonResultViewModel.IsSuccess = true;
        //        //        return new JsonResult(_JsonResultViewModel);
        //        //    }
        //        //    else
        //        //    {
        //        //        _JsonResultViewModel.AlertMessage = "Teacher Creation Failed. " + _ApplicationUser.Item2;
        //        //        _JsonResultViewModel.IsSuccess = false;
        //        //        return new JsonResult(_JsonResultViewModel);
        //        //    }
        //        //}
        //        #endregion

        //        _JsonResultViewModel.AlertMessage = "Operation Failed!!";
        //        _JsonResultViewModel.IsSuccess = false;
        //        return new JsonResult(_JsonResultViewModel);
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return new JsonResult(ex.Message);
        //        throw ex;
        //    }
        //}
        //[HttpGet]
        //public async Task<JsonResult> LoadEducationalLevelBySessionId(Int64 SessionId)
        //{
        //    try
        //    {
        //        var result = await _context.EducationalLevels.Where(x => x.SessionId == SessionId).ToListAsync();
        //        return new JsonResult(result);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpGet]
        //public async Task<JsonResult> LoadSectionByEducationalLevelId(Int64 EducationalLevelId)
        //{
        //    try
        //    {
        //        var result = await _context.Section.Where(x => x.EducationLevelId == EducationalLevelId).ToListAsync();
        //        return new JsonResult(result);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //[HttpGet]
        //public async Task<JsonResult> LoadClassesBySectionId(Int64 SectionId)
        //{
        //    try
        //    {
        //        var result = await _context.ClassInfo.Where(x => x.SectionId == SectionId && x.Cancelled == false).ToListAsync();
        //        return new JsonResult(result);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public async Task<JsonResult> Delete(Int64 id)
        //{
        //    try
        //    {
        //        var _Teacher = await _context.Teacher.FindAsync(id);
        //        string UserName = HttpContext.User.Identity.Name;
        //        _Teacher.ModifiedDate = DateTime.Now;
        //        _Teacher.ModifiedBy = UserName;
        //        _Teacher.Cancelled = true;

        //        _context.Update(_Teacher);
        //        await _context.SaveChangesAsync();

        //        var TeacherClassRelation = _context.ClassInfoTeacher.Where(c => c.TeachersId == id).ToList();
        //        foreach (var relation in TeacherClassRelation)
        //        {
        //            _context.Remove(relation);
        //            await _context.SaveChangesAsync();
        //        }

        //        var TeacherSubjets = _context.TeacherSubjects.Where(t => t.TeacherId == id).ToList();
        //        foreach (var TeacherSubjet in TeacherSubjets)
        //        {
        //            _context.Remove(TeacherSubjet);
        //            await _context.SaveChangesAsync();
        //        }

        //        var result = await _iAccount.DeleteCommonUserProfile
        //            (_Teacher.ApplicationUserId, UserName);

        //        return new JsonResult(_Teacher);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private IQueryable<TeacherCRUDViewModel> GetGridItem(string lang)
        //{
        //    try
        //    {
        //        return (from _Teacher in _context.Teacher
        //                where _Teacher.Cancelled == false
        //                select new TeacherCRUDViewModel
        //                {
        //                    Id = _Teacher.Id,
        //                    ApplicationUserId = _Teacher.ApplicationUserId,
        //                    Name = lang == "ar" ? _Teacher.Name : _Teacher.EnglishName,
        //                    Email = _Teacher.Email,
        //                    PhoneNumber = _Teacher.PhoneNumber,
        //                    Gender = _Teacher.Gender,
        //                    BloodGroup = _Teacher.BloodGroup,
        //                    Address = _Teacher.Address,
        //                    CreatedDate = _Teacher.CreatedDate,
        //                    SubjectId = (long)_Teacher.SubjectId,
        //                    TeacherRoleId = _Teacher.TeacherRoleId,
        //                    ProfileImage = _iCommon.GetAttachmentsLocation(_Teacher.ProfileImageId),
        //                }).OrderByDescending(x => x.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //private IQueryable<TeacherCRUDViewModel> GetByTeacher(Int64 Id)
        //{
        //    try
        //    {
        //        return (from _Teacher in _context.Teacher
        //                join _Designation in _context.Designation on _Teacher.DesignationId equals _Designation.Id
        //                into _Designation
        //                from listDesignation in _Designation.DefaultIfEmpty()
        //                join _Department in _context.Department on _Teacher.DepartmentId equals _Department.Id
        //                into _Department
        //                from listDepartment in _Department.DefaultIfEmpty()
        //                where _Teacher.Cancelled == false && _Teacher.Id == Id
        //                select new TeacherCRUDViewModel
        //                {
        //                    Id = _Teacher.Id,
        //                    Name = _Teacher.Name,
        //                    EnglishName = _Teacher.EnglishName,
        //                    Email = _Teacher.Email,
        //                    DesignationId = _Teacher.DesignationId,
        //                    DesignationDisplay = listDesignation.Name,
        //                    DepartmentId = _Teacher.DepartmentId,
        //                    DepartmentDisplay = listDepartment.Name,
        //                    PhoneNumber = _Teacher.PhoneNumber,
        //                    Gender = _Teacher.Gender,
        //                    BloodGroup = _Teacher.BloodGroup,
        //                    FacebookProfileLink = _Teacher.FacebookProfileLink,
        //                    TwitterProfileLink = _Teacher.TwitterProfileLink,
        //                    LinkedinProfileLink = _Teacher.LinkedinProfileLink,
        //                    Address = _Teacher.Address,
        //                    About = _Teacher.About,
        //                    ProfileImage = _Teacher.ProfileImage,
        //                    CreatedDate = _Teacher.CreatedDate,
        //                    ModifiedDate = _Teacher.ModifiedDate,
        //                    CreatedBy = _Teacher.CreatedBy,
        //                    ModifiedBy = _Teacher.ModifiedBy,
        //                }).OrderByDescending(x => x.Id);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private IQueryable<SectionCRUDViewModel> GetBySession(Int64 Id)
        //{
        //    try
        //    {
        //        return (from _Section in _context.Section


        //                where _Section.SessionId == Id
        //                select new SectionCRUDViewModel
        //                {

        //                    Id = _Section.Id,
        //                    Name = _Section.Name,
        //                    SessionId = _Section.SessionId,
        //                    Description = _Section.Description,
        //                    EducationalLevelId = _Section.EducationLevelId
        //                }).OrderByDescending(x => x.Id);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
