using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.ApiService.Services;
using RentCar.web.Models.Request;

namespace RentCar.web.Controllers;

public class UserController : BaseController
{
    private readonly IUserApiService userApiService;
    private readonly ILogger<UserController> logger;

    public UserController(IUserApiService userApiService, ILogger<UserController> logger)
    {
        this.userApiService = userApiService;
        this.logger = logger;
    }

    public async Task<ActionResult> Index()
    {
        var resp = await userApiService.GetUsers();
        return View(resp.data);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(UserSaveRequest newUser)
    {
        try
        {
            var resp = await userApiService.SaveUser(newUser);
            return RedirectToAction(nameof(Login));
        }
        catch (Exception e)
        {
            return View();
        }
    }

    public ActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginRequest loginRequest)
    {
        try
        {
            var resp = await userApiService.Login(loginRequest);
            //resp.data.Token;
            base.SetSessionUser(resp.data.Token, resp.data.IsAdmin, resp.data.UserId);
            return RedirectToAction(nameof(Index), "Car");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> Logout()
    {
        base.HttpContext.Session.Remove("userId");
        base.HttpContext.Session.Remove("isAdmin");
        base.HttpContext.Session.Remove("token");
        return RedirectToAction(nameof(Index), "Car");
    }
}