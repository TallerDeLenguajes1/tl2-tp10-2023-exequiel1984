using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class TareaController : NuevoController
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _tareaRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;

    }

    public IActionResult Index()
    {
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });

            if (IsAdmin())
            {
                List<Tarea> tareas = _tareaRepository.GetAll();
                return View(tareas);
            } else
            {
                if (IsOperador())
                {
                    int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("id"));
                    Tarea tarea = _tareaRepository.GetById(idUsuario);
                    List<Tarea> tareas = new List<Tarea>();
                    tareas.Add(tarea);
                    return View(tareas);
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
    public IActionResult ListarTareasPorIdTablero(int idTablero)
    {
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });

            List<Tarea> tareas = _tareaRepository.GetAllByIdTablero(idTablero);
            return View(tareas);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        } 
    }

    [HttpGet]
    public IActionResult ListarTareasPorIdUsuario(int idUsuario)
    {
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });

            List<Tarea> tareas = _tareaRepository.GetAllByIdUsuario(idUsuario);
            return View(tareas);
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
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            return View(new Tarea());
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        }
    }

    [HttpPost]
    public IActionResult Crear(Tarea tarea)
    {   
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});

            _tareaRepository.Create(tarea);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        }
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            Tarea tarea = _tareaRepository.GetById(id);
            return View(tarea);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        }
    }

    [HttpPost]
    public IActionResult Editar(Tarea tarea)
    {   
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});

            _tareaRepository.UpDateNombre(tarea.Id, tarea.Nombre);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        }
    }

    
    public IActionResult Eliminar(int id)
    {  
        try
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
            _tareaRepository.Remove(id);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        }
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
