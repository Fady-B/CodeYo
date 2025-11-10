using CodeYoDAL.Models;
using ShaghalnyDAL.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeYoDAL.ViewModels
{
    public class StudentsViewModel : EntityBase
    {
        public Guid Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; } = string.Empty;
        public string SerialNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int TeachersCount { get; set; }
        [Required(ErrorMessage = "Please select at least one teacher!")]
        public List<Guid> TeachersIds { get; set; }
        public List<TeacherViewModel> StudentTeachers { get; set; } = new List<TeacherViewModel>();

        public static implicit operator StudentsViewModel(Students? _Students)
        {
            if (_Students == null)
                return null!;

            return new StudentsViewModel
            {
                Id = _Students.Id,
                ArName = _Students.ArName,
                EnName = _Students.EnName,
                SerialNumber = _Students.SerialNumber,
                UserName = _Students.UserName,
                Password = _Students.Password,

                CreatedDate = _Students.CreatedDate,
                ModifiedDate = _Students.ModifiedDate,
                CreatedBy = _Students.CreatedBy,
                ModifiedBy = _Students.ModifiedBy,
                Cancelled = _Students.Cancelled,
            };
        }

        public static implicit operator Students(StudentsViewModel vm)
        {
            return new Students
            {
                Id = vm.Id,
                ArName = vm.ArName,
                EnName = vm.EnName,
                SerialNumber = vm.SerialNumber,
                UserName = vm.UserName,
                Password = vm.Password,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
