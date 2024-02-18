using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class TareaController : GestorTableroKanbanController
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _tareaRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            if (IsAdmin())
            {
                TareaIndexViewModel tareasVM = new TareaIndexViewModel(_tareaRepository.GetAll(), _tableroRepository.GetAll(), 
                    _usuarioRepository.GetAll());
                return View(tareasVM);
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
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    [HttpGet]
    public IActionResult ListarTareasPorIdTablero(int idTablero)
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            List<Tarea> tareas = _tareaRepository.GetAllByIdTablero(idTablero);
            return View(tareas);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    [HttpGet]
    public IActionResult ListarTareasPorIdUsuario(int idUsuario)
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            List<Tarea> tareas = _tareaRepository.GetAllByIdUsuario(idUsuario);
            return View(tareas);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    [HttpGet]
    public IActionResult TareaCrear()
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            List<Tablero> listaTableros = _tableroRepository.GetAll();
            List<Usuario> listaUsuarios = _usuarioRepository.GetAll();
            return View(new TareaCrearViewModel(listaTableros, listaUsuarios));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult TareaCrear(TareaCrearViewModel tareaVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
        try
        {
            Tarea tarea = new Tarea(tareaVM);
            _tareaRepository.Create(tarea);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            Tarea tarea = _tareaRepository.GetById(id);
            TareaEditarViewModel tareaVM = new TareaEditarViewModel(tarea);
            tareaVM.Tableros = _tableroRepository.GetAll();
            tareaVM.Usuarios = _usuarioRepository.GetAll();
            return View(tareaVM);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Editar(Tarea tarea)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
        try
        {
            _tareaRepository.UpDate(tarea);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
        try
        {    
            _tareaRepository.Remove(id);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
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
