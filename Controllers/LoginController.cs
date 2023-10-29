﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using tl2_tp10_2023_exequiel1984.Models;
using tl2_tp10_2023_exequiel1984.ViewModels;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginUsuario)
    {
        Usuario usuarioLogueado = _usuarioRepository.GetUsuarioLogin(loginUsuario.Nombre, loginUsuario.Contrasenia);
        if (!string.IsNullOrEmpty(usuarioLogueado.NombreDeUsuario))
        {
            LoguearUsuario(usuarioLogueado);
            return RedirectToRoute(new{Controller = "Tablero", action = "Index"});
        } else
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
    }

    private void LoguearUsuario(Usuario usuario)
    {
        HttpContext.Session.SetInt32("id", usuario.Id);
        HttpContext.Session.SetString("usuario", usuario.NombreDeUsuario);
        HttpContext.Session.SetString("contrasenia", usuario.Contrasenia);
        HttpContext.Session.SetString("rol", usuario.Rol.ToString());
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
