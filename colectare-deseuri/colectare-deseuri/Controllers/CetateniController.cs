using colectare_deseuri.Data;
using colectare_deseuri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_deseuri.Controllers
{
    public class CetateniController : Controller
    {
        private readonly AppDbContext _context;

        public CetateniController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListaCetateni()
        {
            var cetateni = await _context.Cetateni.ToListAsync();
            return View(cetateni);
        }

        public IActionResult Adauga()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Cetatean cetatean)
        {
            if (ModelState.IsValid)
            {
                _context.Cetateni.Add(cetatean);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cetățean adăugat cu succes!";
                return RedirectToAction(nameof(ListaCetateni));
            }
            return View(cetatean);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sterge(int id)
        {
            var cetatean = await _context.Cetateni.FindAsync(id);
            if (cetatean != null)
            {
                _context.Cetateni.Remove(cetatean);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cetățean șters cu succes!";
            }
            return RedirectToAction(nameof(ListaCetateni));
        }
        public IActionResult PubeleCetatean(int id)
        {
            var pubele = _context.PubeleCetateni
                .Include(p => p.Pubela)
                .Include(p => p.Cetatean)
                .Where(p => p.IdCetatean == id)
                .ToList();

            ViewBag.Nume = pubele.FirstOrDefault()?.Cetatean.Nume + " " + pubele.FirstOrDefault()?.Cetatean.Prenume;
            return View(pubele);
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("isAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

    }
}
