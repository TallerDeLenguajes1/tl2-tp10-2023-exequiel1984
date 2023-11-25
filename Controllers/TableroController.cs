using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepository _tableroRepository;


    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;

    }

    public IActionResult Index()
    {
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
        {
            if (HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString()) 
            {
                List<Tablero> tableros = _tableroRepository.GetAll(); 
                return View(tableros);
            }
            else
            {
                if (HttpContext.Session.GetString("rol") == NivelDeAcceso.operador.ToString()) 
                {
                    List<Tablero> tableros = _tableroRepository.GetAllByIdUsuario(HttpContext.Session.GetInt32("id").Value);
                    return View(tableros);
                }
            }
        }
        return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Crear()
    {   
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            return View(new CrearTableroViewModel());
        else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpPost]
    public IActionResult Crear(CrearTableroViewModel tableroVM)
    {   
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
        {
            if (HttpContext.Session.GetString("rol") == NivelDeAcceso.operador.ToString()) 
            {
                if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
                Tablero tablero = new Tablero(tableroVM);
                _tableroRepository.Create(tablero);
                return RedirectToAction("Index");
            } else
                return RedirectToRoute(new{Controller = "Login", action = "Index"});
        } else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
        {
            EditarTableroViewModel tableroVM = new EditarTableroViewModel(_tableroRepository.GetById(id));
            return View(tableroVM);
        } else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpPost]
    public IActionResult Editar(EditarTableroViewModel tableroVM)
    {   
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
        {
            if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
            Tablero tablero = new Tablero(tableroVM);
            _tableroRepository.UpDate(tablero);
            return RedirectToAction("Index");
        } else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    
    public IActionResult Eliminar(int id)
    {  
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
        {
            _tableroRepository.Remove(id);
            return RedirectToAction("Index");
        } else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
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
