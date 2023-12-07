using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_exequiel1984.Models;

namespace tl2_tp10_2023_exequiel1984.Controllers;

public class NuevoController : Controller
{
    public bool IsLoged() => !String.IsNullOrEmpty(HttpContext.Session.GetString("usuario"));
    public bool IsAdmin() => HttpContext.Session.GetString("rol") == NivelDeAcceso.administrador.ToString();
    public bool IsOperador() => HttpContext.Session.GetString("rol") == NivelDeAcceso.operador.ToString();
}
