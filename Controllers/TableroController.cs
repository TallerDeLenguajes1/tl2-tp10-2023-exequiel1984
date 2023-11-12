using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;

    private ITableroRepository tableroRepository;


    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();

    }

    public IActionResult Index()
    {
        List<Tablero> tableros = tableroRepository.GetAll();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult Crear()
    {   
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult Crear(Tablero tablero)
    {   
        tableroRepository.Create(tablero);
        return RedirectToAction("Index");
    }
/*
    [HttpGet]
    public IActionResult Editar(int id)
    {  
        Usuario usuario = usuarioRepository.GetById(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult Editar(int id, Usuario usuario)
    {   
        usuarioRepository.Update(id, usuario);

        return RedirectToAction("Index");
    }

    
    public IActionResult Eliminar(int id)
    {  
        usuarioRepository.Remove(id);
        return RedirectToAction("Index");
    } */

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
