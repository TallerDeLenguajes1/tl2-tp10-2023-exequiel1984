using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class TableroController : GestorTableroKanbanController
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITareaRepository _tareaRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        _tareaRepository = tareaRepository;
    }

        public IActionResult Index()
        {
            if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
            try
            {   
                //Hacer switch con GetRol
                
                if (IsAdmin()) 
                {
                    TableroIndexViewModel tablerosVM = new TableroIndexViewModel(_tableroRepository.GetAll()); 
                    foreach (var tablero in tablerosVM.TablerosViewModel)
                    {
                        tablero.NombreUsuarioPropietario = _usuarioRepository.GetNameById(tablero.IdUsuarioPropietario);
                        tablero.tienePermisoDeEdicion = true;
                    }
                    return View(tablerosVM);
                }
                else if (IsOperador())
                {
                    TableroIndexViewModel tablerosVM = new TableroIndexViewModel(_tableroRepository.GetByIdUsuarioPropietario(HttpContext.Session.GetInt32("id").Value));
                    foreach (var tablero in tablerosVM.TablerosViewModel)
                        tablero.tienePermisoDeEdicion = true;
                    
                    List<int> listaIdTablerosAsignados = new List<int>();
                    listaIdTablerosAsignados = _tareaRepository.GetListIdTableroByIdUsuario(HttpContext.Session.GetInt32("id").Value);
                    foreach (var idTablero in listaIdTablerosAsignados)
                    {
                        if (tablerosVM.TablerosViewModel.Any(t => t.Id == idTablero))
                            continue;
    
                        Tablero tablero = new Tablero();
                        tablero = _tableroRepository.GetById(idTablero);
                        TableroElementoIndexViewModel tableroVM = new TableroElementoIndexViewModel(tablero);
                        tablerosVM.TablerosViewModel.Add(tableroVM);
                    }

                    foreach (var tablero in tablerosVM.TablerosViewModel)
                        tablero.NombreUsuarioPropietario = _usuarioRepository.GetNameById(tablero.IdUsuarioPropietario);
                    
                    return View(tablerosVM);
                } else  
                    return RedirectToRoute(new { Controller = "Login", action = "Index" });
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

    [HttpGet]
    //[Route("Tablero/Crear")]
    public IActionResult Crear()
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {    
            if(IsOperador())
                return View("CrearPorOperador", new TableroCrearPorOperadorViewModel(HttpContext.Session.GetInt32("id").Value));
            
            List<Usuario> usuarios = _usuarioRepository.GetAll();
            return View(new TableroCrearViewModel(usuarios));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Crear(TableroCrearViewModel tableroVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Tablero", action = "Crear"});
        if (!IsAdmin()) return RedirectToRoute(new{Controller = "Tablero", action = "Crear"});    
        try
        {    
            Tablero tablero = new Tablero(tableroVM);
            _tableroRepository.Create(tablero);
            return RedirectToAction("Index");  
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    [HttpPost]
    public IActionResult CrearPorOperador(TableroCrearPorOperadorViewModel tableroVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Tablero", action = "Crear"});  
        try
        {    
            Tablero tablero = new Tablero(tableroVM);
            _tableroRepository.Create(tablero);
            return RedirectToAction("Index");  
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    /* [HttpGet]
    public IActionResult GetTableroById()
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {    
            return View(new GetTableroByIdViewModel());
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    } */

    /* [HttpPost]
    public IActionResult TableroPorId(GetTableroByIdViewModel tableroVM)
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
        try
        {    
            Tablero tablero = _tableroRepository.GetById(tableroVM.Id);
            TableroPorIdViewModel tableroPorIdVM = new TableroPorIdViewModel(tablero);
            return View(tableroPorIdVM);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    } */

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            Tablero tablero = _tableroRepository.GetById(id);
            TableroEditarViewModel tableroVM = new TableroEditarViewModel(tablero);
            tableroVM.Usuarios = _usuarioRepository.GetAll();
            return View(tableroVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Editar(TableroEditarViewModel tableroVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Tablero", action = "Editar"});
        try
        {   
            Tablero tablero = new Tablero(tableroVM);
            _tableroRepository.UpDate(tablero);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]//porque viene de link, si viene de form por submit es POST
    public IActionResult Eliminar(int id)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Tablero", action = "Index"});
        try
        {
            _tableroRepository.Remove(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
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
