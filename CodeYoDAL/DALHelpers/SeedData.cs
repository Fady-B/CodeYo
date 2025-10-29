using Microsoft.AspNetCore.Identity;
using ShaghalnyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeYoDAL.DALHelpers
{
    public class SeedData
    {
        //public static IEnumerable<IdentityRole> GetIdentityRoles()
        //{
        //    return new List<IdentityRole>
        //    {
        //        new IdentityRole
        //        {
        //            Id = "8420350d-159f-4fe8-a729-5d72b5179884",
        //            Name = "Super Admin",
        //            NormalizedName = "SUPER ADMIN",
        //            ConcurrencyStamp = "e6648bfc-6c73-491b-81a3-b4000fb382e9"
        //        },
        //        new IdentityRole
        //        {
        //            Id = "3406c413-b2da-4599-8523-aeff97777549",
        //            Name = "Admin",
        //            NormalizedName = "ADMIN",
        //            ConcurrencyStamp = "bbc527bc-ea03-4b17-a8f8-4a9db04483a3"
        //        },
        //        new IdentityRole
        //        {
        //            Id = "23e23f22-5cc6-4a22-a6be-b9c9eecaa491",
        //            Name = "Supervisor",
        //            NormalizedName = "SUPERVISOR",
        //            ConcurrencyStamp = "9da54755-c68b-4226-a568-f9098ec7aa74"
        //        },
        //        new IdentityRole
        //        {
        //            Id = "7a871170-3e96-4ab3-a76b-aec96092a618",
        //            Name = "Home Page",
        //            NormalizedName = "HOME PAGE",
        //            ConcurrencyStamp = "322f0125-66de-4d30-b093-a89f4b59384e"
        //        },
        //        new IdentityRole
        //        {
        //            Id = "f8f08728-2742-45e5-a222-cc9b889c2b21",
        //            Name = "Students",
        //            NormalizedName = "STUDENTS",
        //            ConcurrencyStamp = "b2ac13ff-1145-4331-8e7e-2167e2cb92e0"
        //        },
        //        new IdentityRole
        //        {
        //            Id = "32f77820-adbd-4bf4-8bb9-c0668b6fa55e",
        //            Name = "Design Card",
        //            NormalizedName = "DESIGN CARD",
        //            ConcurrencyStamp = "4427b4f1-cf74-470a-a013-49461a1bd4e4"
        //        }
        //    };
        //}
        public static IEnumerable<UserType> GetUserTypes()
        {
            return new List<UserType>
            {
                new UserType{Id=1, Name="Super Admin"},
                new UserType{Id=2, Name="Admin"},
                new UserType{Id=3, Name="Supervisor"},
                new UserType{Id=4, Name="Regular"},
            };
        }
    }
}
