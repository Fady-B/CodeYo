using CodeYoDAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ShaghalnyDAL.Models
{
    public class TeacherStudents
    {
        [Key]
        public Guid StudentId { get; set; }
        public Students Student { get; set; }
        [Key]
        public Guid TeacherId { get; set; }
        public Teachers Teacher { get; set; }


    }
}
