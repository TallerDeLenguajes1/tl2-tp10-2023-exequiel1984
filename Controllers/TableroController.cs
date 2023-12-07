using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class NuevoController : Controller
{
    public bool IsLoged() => !String.IsNullOrEmpty(HttpContext.Session.GetString("usuario"));
    public bool IsAdmin() => HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString();
    public bool IsOperador() => HttpContext.Session.GetString("rol") == NivelDeAcceso.operador.ToString();
}

public class TableroController : NuevoController
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            if (IsAdmin()) 
            {
                List<Tablero> tableros = _tableroRepository.GetAll(); 
                return View(tableros);
            }
            else
            {
                if (IsOperador()) 
                {
                    List<Tablero> tableros = _tableroRepository.GetAllByIdUsuario(HttpContext.Session.GetInt32("id").Value);
                    return View(tableros);
                } else  
                    return RedirectToRoute(new { Controller = "Login", action = "Index" });
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        }
    }

    [HttpGet]
    public IActionResult Crear()
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        List<Usuario> usuarios = _usuarioRepository.GetAll();
        return View(new CrearTableroViewModel(usuarios));
    }

    [HttpPost]
    public IActionResult Crear(CrearTableroViewModel tableroVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});

        if (IsAdmin()) 
        {
            Tablero tablero = new Tablero(tableroVM);
            _tableroRepository.Create(tablero);
            return RedirectToAction("Index");
        } else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            Tablero tablero = _tableroRepository.GetById(id);
            EditarTableroViewModel tableroVM = new EditarTableroViewModel(tablero);
            return View(tableroVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        }
        
    }


    [HttpPost]
    public IActionResult Editar(EditarTableroViewModel tableroVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
        
        Tablero tablero = new Tablero(tableroVM);
        _tableroRepository.UpDate(tablero);
        return RedirectToAction("Index");
    }

    
    public IActionResult Eliminar(int id)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        _tableroRepository.Remove(id);
        return RedirectToAction("Index");
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
