using System.ComponentModel.DataAnnotations;

namespace ShaghalnyDAL.Models
{
    public class TeacherStudents
    {
        [Key]
        public Guid StudentId { get; set; }
        [Key]
        public Guid TeacherId { get; set; }
    }
}
