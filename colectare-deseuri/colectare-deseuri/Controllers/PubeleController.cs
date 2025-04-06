using colectare_deseuri.Data;
using colectare_deseuri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace colectare_deseuri.Controllers
{
    public class PubeleController : Controller // <-- modificat de la ControllerBase
    {
        private readonly AppDbContext _context;
        public PubeleController(AppDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Atribuire(int cetateanId)
        {
            var model = new PubelaCetatean
            {
                IdCetatean = cetateanId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtribuiePubela([FromForm] PubelaCetatean model)
        {
            var pubela = await _context.Pubele.FindAsync(model.IdPubela);
            if (pubela == null)
            {
                pubela = new Pubela { Id = model.IdPubela };
                _context.Pubele.Add(pubela);
                await _context.SaveChangesAsync();
            }

            model.Pubela = pubela;

            _context.PubeleCetateni.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Lista", new { cetateanId = model.IdCetatean });
        }

        [HttpGet]
        public async Task<IActionResult> Lista(int cetateanId)
        {
            var pubele = await _context.PubeleCetateni
                .Where(pc => pc.IdCetatean == cetateanId)
                .Include(pc => pc.Pubela)
                .ToListAsync();

            var cetatean = await _context.Cetateni.FindAsync(cetateanId);
            ViewBag.NumeComplet = cetatean?.Nume + " " + cetatean?.Prenume;
            ViewBag.CetateanId = cetateanId;

            return View("ListaPubele", pubele); // ATENȚIE: numele view-ului
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sterge(int idPubela, int idCetatean)
        {
            var legatura = await _context.PubeleCetateni
                .FirstOrDefaultAsync(p => p.IdPubela == idPubela && p.IdCetatean == idCetatean);

            if (legatura != null)
            {
                _context.PubeleCetateni.Remove(legatura);

                // Poți șterge și pubela dacă nu mai e legată de nimeni
                var alteLegaturi = await _context.PubeleCetateni
                    .CountAsync(p => p.IdPubela == idPubela);

                if (alteLegaturi <= 1)
                {
                    var pubela = await _context.Pubele.FindAsync(idPubela);
                    if (pubela != null)
                        _context.Pubele.Remove(pubela);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Lista", new { cetateanId = idCetatean });
        }

    }
}
