using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using colectare_deseuri.Models;
using Microsoft.EntityFrameworkCore;
using colectare_deseuri.Data;

namespace colectare_deseuri.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var colectari = await _context.Colectari.ToListAsync();
        return View(colectari);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
