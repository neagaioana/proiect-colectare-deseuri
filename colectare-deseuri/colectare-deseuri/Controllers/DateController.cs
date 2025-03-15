using colectare_deseuri.Data;
using colectare_deseuri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_deseuri.Controllers
{
    
[Route("api/[controller]")]
[ApiController]
    public class DateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DateController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostDate([FromBody] Date colectare)
        {
            if (colectare == null)
            {
                return BadRequest("Datele nu sunt valide.");
            }

            // Salvează colectarea în baza de date
            _context.Colectari.Add(colectare);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Date salvate: {colectare.IdPubela}, {colectare.CollectionTime}");

            return Ok(new { message = "Datele au fost adăugate cu succes!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetDate()
        {
            var allData = await _context.Colectari.ToListAsync();
            return Ok(allData);
        }
    }
}



