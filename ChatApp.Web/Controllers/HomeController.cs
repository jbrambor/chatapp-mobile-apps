using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Web.Models;

namespace ChatApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private const string SessionKey = "USER_SESSION";
    private const string UserName = "USER_NAME";
    private const string UserSurname = "USER_SURNAME";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        var login = HttpContext.Session.GetString(SessionKey);

        if (login != null)
        {
            return RedirectToAction("ChatApp");
        }


        var vm = new User();
        return View(vm);
    }

    [HttpPost]
    public IActionResult SignIn(User user)
    {

        if (!ModelState.IsValid)
        {
            return View(user);
        }

        HttpContext.Session.SetString(SessionKey, string.Concat(user.Name, '_', user.Surname));
        HttpContext.Session.SetString(UserName, user.Name);
        HttpContext.Session.SetString(UserSurname, user.Surname);
        return RedirectToAction("ChatApp");
    }

    [HttpGet]
    public IActionResult LogOut()
    {
        HttpContext.Session.Remove(SessionKey);
        return RedirectToAction("SignIn");
    }

    public IActionResult ChatApp()
    {
        var login = HttpContext.Session.GetString(SessionKey);
        var name = HttpContext.Session.GetString(UserName);
        var surname = HttpContext.Session.GetString(UserSurname);

        if (login == null || name == null || surname == null)
        {
            return RedirectToAction("SignIn");
        }

        var vm = new User()
        {
            Name = name,
            Surname = surname
        };

        return View(vm);
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