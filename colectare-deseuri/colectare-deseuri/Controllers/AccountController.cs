using Microsoft.AspNetCore.Mvc;

namespace colectare_deseuri.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string parola)
        {
            if (username == "admin" && parola == "admin12")
            {
                HttpContext.Session.SetString("admin", "true");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Eroare = "Date de autentificare invalide.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
