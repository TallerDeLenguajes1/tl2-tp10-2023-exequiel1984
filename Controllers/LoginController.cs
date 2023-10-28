using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;
namespace tl2_tp10_2023_exequiel1984.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    //private IUsuarioRepository usuarioRepository;


    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;

    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    /* [HttpGet]
    public IActionResult CrearUsuario()
    {   
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {   
        usuarioRepository.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {  
        Usuario usuario = usuarioRepository.GetById(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult Editar(Usuario usuario)
    {   
        usuarioRepository.Update(usuario);

        return RedirectToAction("Index");
    }

    
    public IActionResult Eliminar(int id)
    {  
        usuarioRepository.Remove(id);
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
    } */
}
