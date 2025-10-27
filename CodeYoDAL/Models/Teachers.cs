using Microsoft.AspNetCore.Identity;
using ShaghalnyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoDAL.Models
{
    public class Teachers : EntityBase
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string SubjectName { get; set; }
        public TeacherCardsAttachements TeacherCardsAttachement{ get; set; }
    }
}
