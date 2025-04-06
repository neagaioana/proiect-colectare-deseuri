using colectare_deseuri.Data;
using Microsoft.AspNetCore.Mvc;

namespace colectare_deseuri.Controllers
{
    public class HartaController : Controller
    {
        private readonly AppDbContext _context;

        public HartaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Colectari()
        {
            var colectari = _context.Colectari
                .Where(c => c.TimpRidicare.StartsWith("2024-10-15"))
                .Select(c => new
                {
                    adresa = c.Adresa,
                    latitude = c.Latitude,
                    longitude = c.Longitude,
                    timpRidicare = c.TimpRidicare
                })
                .ToList();

            ViewBag.Colectari = colectari;
            return View("HartaColectari");
        }


    }
}
