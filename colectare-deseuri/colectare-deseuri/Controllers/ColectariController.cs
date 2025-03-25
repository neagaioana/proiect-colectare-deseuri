using colectare_deseuri.Data;
using colectare_deseuri.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colectare_deseuri.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColectareController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColectareController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Colectare colectare)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Colectari.Add(colectare);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Colectare adăugată cu succes!" });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var date = await _context.Colectari.ToListAsync();
            return Ok(date);
        }
    }
}
