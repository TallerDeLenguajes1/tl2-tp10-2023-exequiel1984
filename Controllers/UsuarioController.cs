using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class UsuarioController : GestorTableroKanbanController
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            if (IsAdmin())
            {
                UsuarioIndexViewModel usuariosVM = new UsuarioIndexViewModel(_usuarioRepository.GetAll()) ;
                return View(usuariosVM);
            } else
            {
                if (IsOperador())
                {
                    int idUsuario = HttpContext.Session.GetInt32("id").Value;
                    Usuario usuario = _usuarioRepository.GetById(idUsuario);
                    List<Usuario> ListaUsuarios = new List<Usuario>();
                    ListaUsuarios.Add(usuario);
                    UsuarioIndexViewModel usuarios = new UsuarioIndexViewModel(ListaUsuarios) ;
                    return View(usuarios);
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
    public IActionResult CrearUsuario()
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if (!IsAdmin()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        try
        {
            return View(new UsuarioCrearViewModel());
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        } 
    }

    [HttpPost]
    public IActionResult CrearUsuario(UsuarioCrearViewModel usuarioVM)
    {   
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if (!IsAdmin()) return RedirectToRoute(new{Controller = "Login", action = "Index"}); 
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Usuario", action = "CrearUsuario"});
        
        try
        {
            Usuario usuario = new Usuario(usuarioVM);
            _usuarioRepository.Create(usuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
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
            if (IsOperador())
            {
                UsuarioEditarOperadorViewModel usuarioEditarOperador = new UsuarioEditarOperadorViewModel(_usuarioRepository.GetById(id));
                return View("EditarPorOperador",usuarioEditarOperador);
            }
            UsuarioEditarViewModel usuarioEditar = new UsuarioEditarViewModel(_usuarioRepository.GetById(id));
            return View(usuarioEditar);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Editar(UsuarioEditarViewModel usuarioVM)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if (!IsAdmin()) return RedirectToRoute(new{Controller = "Login", action = "Index"}); 
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Usuario", action = "Editar"});
        try
        {
            Usuario usuario = new Usuario(usuarioVM);
            _usuarioRepository.Update(usuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult EditarPorOperador(UsuarioEditarOperadorViewModel usuarioVM)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" }); 
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Usuario", action = "Editar"});
        try
        {
            Usuario usuario = new Usuario(usuarioVM);
            _usuarioRepository.Update(usuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {  
        if (!IsLoged()) return RedirectToRoute(new { Controller = "Login", action = "Index" });
        if (!IsAdmin()) return RedirectToRoute(new{Controller = "Login", action = "Index"}); 
        if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Usuario", action = "Index"});
        try
        {
            _usuarioRepository.Remove(id);
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
