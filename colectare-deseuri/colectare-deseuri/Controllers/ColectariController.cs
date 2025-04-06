using colectare_deseuri.Data;
using colectare_deseuri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_deseuri.Controllers
{
    public class ColectariController : Controller
    {
        private readonly AppDbContext _context;

        public ColectariController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListaColectari()
        {
            var colectari = await _context.Colectari.ToListAsync();
            return View(colectari);
        }
    }
}
