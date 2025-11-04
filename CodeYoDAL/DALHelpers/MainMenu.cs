using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeYoDAL.DALHelpers;

namespace CodeYoDAL.DALHelpers
{
    public static class MainMenu
    {
        public static class Teachers
        {
            public const string PageName = "Teachers Management";
            public const string RoleName = "Teachers";
            public const string Path = "/Teachers/Index";
            public const string ControllerName = "Teachers";
            public const string ActionName = "Index";
        }

        public static class Students
        {
            public const string PageName = "Students Management";
            public const string RoleName = ApplicationRoles.Students;
            public const string Path = "/Students/Index";
            public const string ControllerName = "Students";
            public const string ActionName = "Index";
        }

        public static class Home
        {
            public const string PageName = "Home";
            public const string RoleName = ApplicationRoles.HomePage;
            public const string Path = "/Home/Index";
            public const string ControllerName = "Students";
            public const string ActionName = "Index";
        }

        public static class DesignCard
        {
            public const string PageName = "Design Students Cards";
            public const string RoleName = ApplicationRoles.DesignCard;
            public const string Path = "/CardsDesign/Index";
            public const string ControllerName = "CardsDesign";
            public const string ActionName = "Index";
        }
    }
}
