using colectare_deseuri.Data;
using colectare_deseuri.Models;
using Microsoft.AspNetCore.Mvc;

namespace colectare_deseuri.Controllers
{
    [ApiController]
    [Route("api/pubele")]
    public class PubeleController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PubeleController(AppDbContext context) => _context = context;

        [HttpPost("atribuire")]
        public async Task<IActionResult> AtribuiePubela([FromBody] PubelaCetatean model)
        {
            var pubela = await _context.Pubele.FindAsync(model.IdPubela);
            if (pubela == null)
            {
                pubela = new Pubela { Id = model.IdPubela };
                _context.Pubele.Add(pubela);
                await _context.SaveChangesAsync();
            }

            _context.PubeleCetateni.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }
    }
}
