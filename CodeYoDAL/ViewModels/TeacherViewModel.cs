using CodeYoDAL.Models;
using ShaghalnyDAL.Models;

namespace CodeYoDAL.ViewModels
{
    public class TeacherViewModel : EntityBase
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string SubjectName { get; set; }
        public int StudentsCount { get; set; }
        public List<StudentsViewModel> TeacherStudents { get; set; } = new List<StudentsViewModel>();

        public static implicit operator TeacherViewModel(Teachers _Teacher)
        {
            return new TeacherViewModel
            {
                Id = _Teacher.Id,
                FullName = _Teacher.FullName,
                PersonalPhoneNumber = _Teacher.PersonalPhoneNumber,
                BusinessPhoneNumber = _Teacher.BusinessPhoneNumber,
                SubjectName = _Teacher.SubjectName,

                CreatedDate = _Teacher.CreatedDate,
                ModifiedDate = _Teacher.ModifiedDate,
                CreatedBy = _Teacher.CreatedBy,
                ModifiedBy = _Teacher.ModifiedBy,
                Cancelled = _Teacher.Cancelled,
            };
        }

        public static implicit operator Teachers(TeacherViewModel vm)
        {
            return new Teachers
            {
                Id = vm.Id,
                FullName = vm.FullName,
                PersonalPhoneNumber = vm.PersonalPhoneNumber,
                BusinessPhoneNumber = vm.BusinessPhoneNumber,
                SubjectName = vm.SubjectName,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,
            };
        }
    }
}
