using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;

    private ITareaRepository tareaRepository;


    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();

    }

    public IActionResult Index()
    {
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
        {
            if (HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString())
            {
                List<Tarea> tareas = tareaRepository.GetAll();
                return View(tareas);
            } else
            {
                if (HttpContext.Session.GetString("rol") == NivelDeAcceso.operador.ToString())
                {
                    int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("id"));
                    Tarea tarea = tareaRepository.GetById(idUsuario);
                    List<Tarea> tareas = new List<Tarea>();
                    tareas.Add(tarea);
                    return View(tareas);
                }
            }
        }
        return RedirectToRoute(new{Controller = "Login", action = "Index"});
        
    }

    [HttpGet]
    public IActionResult ListarTareasPorIdTablero(int idTablero)
    {
        List<Tarea> tareas = tareaRepository.GetAllByIdTablero(idTablero);
        return View(tareas);
    }

    [HttpGet]
    public IActionResult ListarTareasPorIdUsuario(int idUsuario)
    {
        List<Tarea> tareas = tareaRepository.GetAllByIdUsuario(idUsuario);
        return View(tareas);
    }

    [HttpGet]
    public IActionResult Crear()
    {   
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult Crear(Tarea tarea)
    {   
        tareaRepository.Create(tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        Tarea tarea = tareaRepository.GetById(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult Editar(Tarea tarea)
    {   
        tareaRepository.UpDateNombre(tarea.Id, tarea.Nombre);

        return RedirectToAction("Index");
    }

    
    public IActionResult Eliminar(int id)
    {  
        tareaRepository.Remove(id);
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
