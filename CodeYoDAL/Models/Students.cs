using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoDAL.Models
{
    public class Students : EntityBase
    {
        public Guid Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string SerialNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
