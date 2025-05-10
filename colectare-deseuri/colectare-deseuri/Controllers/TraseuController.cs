using colectare_deseuri.Data;
using colectare_deseuri.Models;
using colectare_deseuri.Services;
using Microsoft.AspNetCore.Mvc;

namespace colectare_deseuri.Controllers
{
    public class TraseuController : Controller
    {
        private readonly AppDbContext _context;

        public TraseuController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult RutaOptimizata()
        {
            var toateColectarile = _context.Colectari.OrderBy(c => c.TimpRidicare).ToList();

            var start = toateColectarile.FirstOrDefault(c => c.Adresa.Contains("Șelimbărului 90"));
            var stop = toateColectarile.FirstOrDefault(c => c.Adresa.Contains("Cristian"));

            if (start == null || stop == null)
            {
                ViewBag.Eroare = "Nu s-au găsit punctele START și STOP!";
                return View("Eroare");
            }

            var puncteIntermediare = toateColectarile.Where(c => c != start && c != stop).ToList();

            string caleFisier = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/Traseu3.xlsx");
            var matrice = MatrixLoader.LoadMatrix(caleFisier);

            if (toateColectarile.Count != matrice.GetLength(0))
            {
                ViewBag.Eroare = "Matricea nu are același număr de puncte ca baza de date!";
                return View("Eroare");
            }

            // Ordinea optimizată DOAR pentru punctele intermediare
            var indexStart = toateColectarile.IndexOf(start);
            var indexStop = toateColectarile.IndexOf(stop);

            var indexeIntermediare = Enumerable.Range(0, toateColectarile.Count)
                .Where(i => i != indexStart && i != indexStop)
                .ToList();

            var optimIntermediare = TspSolver.NearestNeighborPartial(matrice, indexStart, indexStop, indexeIntermediare);

            var colectariOptimizate = new List<Colectare> { start };
            colectariOptimizate.AddRange(optimIntermediare.Select(i => toateColectarile[i]));
            colectariOptimizate.Add(stop);

            double distInit = TspSolver.CalculeazaDistantaTotala(matrice, Enumerable.Range(0, toateColectarile.Count).ToList()) / 1000.0;

            ViewData["DistantaInitiala"] = Math.Round(distInit, 2);
            ViewData["TotalPuncte"] = toateColectarile.Count;

            return View("RutaOptimizata", (Original: toateColectarile, Optimizata: colectariOptimizate));
        }

    }
}
