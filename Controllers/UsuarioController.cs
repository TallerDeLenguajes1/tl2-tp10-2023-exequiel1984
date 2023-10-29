using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;

    private readonly IUsuarioRepository _usuarioRepository;


    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        this._usuarioRepository = usuarioRepository;

    }

    public IActionResult Index()
    {
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
        {
            if (HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString())
            {
                IndexUsuarioViewModel usuarios = new IndexUsuarioViewModel(_usuarioRepository.GetAll()) ;
                return View(usuarios);
            } else
            {
                if (HttpContext.Session.GetString("rol") == NivelDeAcceso.operador.ToString())
                {
                    int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("id"));
                    Usuario usuario = _usuarioRepository.GetById(idUsuario);
                    List<Usuario> ListaUsuarios = new List<Usuario>();
                    ListaUsuarios.Add(usuario);
                    IndexUsuarioViewModel usuarios = new IndexUsuarioViewModel(ListaUsuarios) ;
                    return View(usuarios);
                }
            }
        }
        return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {   
        if (HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString()) 
            return View(new UsuarioCrearViewModel());
        else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpPost]
    public IActionResult CrearUsuario(UsuarioCrearViewModel usuarioVM)
    {   
        if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
        {
            if (HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString()) 
            {
                if(!ModelState.IsValid) return RedirectToRoute(new{Controller = "Login", action = "Index"});
                Usuario usuario = new Usuario (usuarioVM);
                _usuarioRepository.Create(usuario);
                return RedirectToAction("Index");
            } else
                return RedirectToRoute(new{Controller = "Login", action = "Index"});

        } else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        UsuarioEditarViewModel usuarioEditar = new UsuarioEditarViewModel(_usuarioRepository.GetById(id));
        return View(usuarioEditar);
    }

    [HttpPost]
    public IActionResult Editar(UsuarioEditarViewModel usuarioVM)
    {  
        if (HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString()) 
        { 
            Usuario usuario = new Usuario(usuarioVM);
            _usuarioRepository.Update(usuario);
            return RedirectToAction("Index");
        }
        else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    
    public IActionResult Eliminar(int id)
    {  
        if (HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString()) 
        {
            _usuarioRepository.Remove(id);
            return RedirectToAction("Index");
        }
        else
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
