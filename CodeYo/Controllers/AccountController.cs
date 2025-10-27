using Microsoft.AspNetCore.Mvc;

namespace CodeYo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
