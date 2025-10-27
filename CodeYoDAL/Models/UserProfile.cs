using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoDAL.Models
{
    public class UserProfile : EntityBase
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ApplicationUserId { get; set; }
        public int UserType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string? Address { get; set; }
    }
}
