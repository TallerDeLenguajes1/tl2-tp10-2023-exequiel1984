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
                TareaIndexViewModel tareasVM = new TareaIndexViewModel(_tareaRepository.GetAll());
                foreach (var tarea in tareasVM.TareasViewModel)
                {
                    tarea.NombreTablero = _tableroRepository.GetNameById(tarea.IdTablero);
                    tarea.NombreUsuarioAsignado = _usuarioRepository.GetNameById(tarea.IdUsuarioAsignado);
                    int idUsuarioPropietario = _tableroRepository.GetIdUsuarioPropietarioById(tarea.IdTablero);
                    tarea.NombreUsuarioPropietario = _usuarioRepository.GetNameById(idUsuarioPropietario);
                    tarea.tienePermisoDeEdicion = true;
                }
                return View(tareasVM);
            } else
            {
                if (IsOperador())
                {
                    int idUsuarioLogueado = HttpContext.Session.GetInt32("id").Value;
                    
                    TareaIndexViewModel tareasVM = new TareaIndexViewModel(_tareaRepository.GetByIdUsuarioAsignado(idUsuarioLogueado));
                    foreach (var tarea in tareasVM.TareasViewModel)
                    {
                        tarea.NombreTablero = _tableroRepository.GetNameById(tarea.IdTablero);
                        tarea.NombreUsuarioAsignado = _usuarioRepository.GetNameById(tarea.IdUsuarioAsignado);
                        int idUsuarioPropietario = _tableroRepository.GetIdUsuarioPropietarioById(tarea.IdTablero);
                        tarea.NombreUsuarioPropietario = _usuarioRepository.GetNameById(idUsuarioPropietario);
                    }

                    List<int> listaIdTableroPropietario = _tableroRepository.GetListIdByUsuarioPropietario(idUsuarioLogueado);
                    foreach (var idTablero in listaIdTableroPropietario)
                    {
                        List<Tarea> tareasPropias = _tareaRepository.GetAllByIdTablero(idTablero);
                        foreach (var tarea in tareasPropias)
                        {
                            TareaElementoIndexViewModel tareaVM = new TareaElementoIndexViewModel(tarea);
                            tareaVM.NombreTablero = _tableroRepository.GetNameById(tarea.IdTablero);
                            tareaVM.NombreUsuarioAsignado = _usuarioRepository.GetNameById(tarea.IdUsuarioAsignado);
                            int idUsuarioPropietario = _tableroRepository.GetIdUsuarioPropietarioById(tarea.IdTablero);
                            tareaVM.NombreUsuarioPropietario = _usuarioRepository.GetNameById(idUsuarioPropietario);
                            tareaVM.tienePermisoDeEdicion = true;
                            
                            tareasVM.TareasViewModel.Add(tareaVM);
                            
                        }
                    }
                    
                    return View(tareasVM);
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
    public IActionResult ListarTareasPorIdTablero(int idTablero, string nombrePropietarioTablero, int idPropietarioTablero)
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            TareaIndexViewModel tareasVM = new TareaIndexViewModel(_tareaRepository.GetAllByIdTablero(idTablero));
            foreach (var tarea in tareasVM.TareasViewModel)
            {
                tarea.NombreTablero = _tableroRepository.GetNameById(tarea.IdTablero);
                tarea.NombreUsuarioAsignado = _usuarioRepository.GetNameById(tarea.IdUsuarioAsignado);
                int idUsuarioPropietario = _tableroRepository.GetIdUsuarioPropietarioById(tarea.IdTablero);
                tarea.NombreUsuarioPropietario = _usuarioRepository.GetNameById(idUsuarioPropietario);
            }
            tareasVM.NombrePropietarioTablero = nombrePropietarioTablero;

            if (IsAdmin() || idPropietarioTablero == HttpContext.Session.GetInt32("id").Value)
            {
                foreach (var tarea in tareasVM.TareasViewModel)
                    tarea.tienePermisoDeEdicion = true;
            }

            return View(tareasVM);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    /* [HttpGet]
    public IActionResult ListarTareasPorIdUsuario(int idUsuario)
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            List<Tarea> tareas = _tareaRepository.GetByIdUsuarioAsignado(idUsuario);
            return View(tareas);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    } */

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


            int idPropietarioTablero = _tableroRepository.GetIdUsuarioPropietarioById(tarea.IdTablero);

            //Controlar si es operador y tambien si la tarea NO es del operador, ademas verificar si esta asignada
            //Si no es mia y tampoco la tengo asignada (es un error) enviar a un badrequest
            if(!IsAdmin() && idPropietarioTablero != HttpContext.Session.GetInt32("id").Value)
            {
                TareaEditarEstadoViewModel tareaEstadoVM = new TareaEditarEstadoViewModel(tarea); 
                return View("EditarEstado", tareaEstadoVM);
            }

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
    public IActionResult Editar(TareaEditarViewModel tareaVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
        try
        {
            Tarea tarea = new Tarea(tareaVM);
            _tareaRepository.UpDate(tarea);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult EditarEstado(TareaEditarEstadoViewModel tareaVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Tarea", action = "Index"});
        try
        {
            Tarea tarea = new Tarea(tareaVM);
            _tareaRepository.UpDateEstado(tarea.Id, tarea.Estado);
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
