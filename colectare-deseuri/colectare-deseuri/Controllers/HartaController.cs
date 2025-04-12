using colectare_deseuri.Data;
using colectare_deseuri.Models;
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
            
            // 1. Luăm colectările din 15.10.2024
            var colectari = _context.Colectari
                .Where(c => c.TimpRidicare.StartsWith("2024-10-15"))
                .ToList();

            // 2. Adăugăm manual punctele speciale
            colectari.Insert(0, new Colectare
            { 
                Adresa = "Start: Garaj (Strada Șelimbărului 90, Cisnădie)",
                Latitude = 45.7315361f,
                Longitude = 24.1779393f,
                TimpRidicare = "Start",
                IdPubela = "-"
            });

            colectari.Add(new Colectare
            {
                Adresa = "Destinație: Groapa de gunoi (TRACON SRL DEDMI - Cristian)",
                Latitude = 45.7877059f,
                Longitude = 24.0247875f,
                TimpRidicare = "Final",
                IdPubela = "-"
            });

            ViewBag.Colectari = colectari;
            return View("HartaColectari");
        }
    }
}
