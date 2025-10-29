using System;

namespace CodeYoDAL.DALHelpers
{
    public static class StaticData
    {

        public static string ProjectName = "Code-Yo";
        public static string CompanyLogo = "CompanyLogo";

        //Static Data
        public static string DefaultUserProfileImage = "5b05e1bc-24c9-49ad-a9da-471469a1c18b-blank-person.png";
        public static string DefaultUserProfileImagePath = "wwwroot/images/5b05e1bc-24c9-49ad-a9da-471469a1c18b-blank-person.png";

        public static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        public static string GetUniqueID(string Prefix)
        {
            Random _Random = new Random();
            var result = Prefix + DateTime.Now.ToString("yyyyMMddHHmmss") + _Random.Next(1, 1000);
            return result;
        }

        public static List<int> GetMonthNumbersBetweenDates(DateTime startDate, DateTime endDate)
        {
            int monthsDiff = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;

            return Enumerable.Range(0, monthsDiff + 1)
                             .Select(i => startDate.AddMonths(i).Month)
                             .ToList();
        }
        public static List<int> GetYearNumbersBetweenDates(DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(startDate.Year, endDate.Year - startDate.Year + 1).ToList();
        }
    }

    public static class ApplicationRoles
    {
        public const string SuperAdmin = "Super Admin";
        public const string Admin = "Admin";
        public const string Supervisor = "Supervisor";
        public const string DesignCard = "Design Card";
        public const string HomePage = "Home Page";
        public const string Students = "Students";
        public const string Teachers = "Teachers";
    }

    public static class AppUserType
    {
        public const int Regular = 0;
        public const int Student = 1;
        public const int Teacher = 2;
        public const int Parent = 3;
        public const int Librarian = 4;
        public const int Accountant = 5;
        public const int Admin = 6;
    }



}
