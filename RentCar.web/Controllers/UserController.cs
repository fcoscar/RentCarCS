using Microsoft.AspNetCore.Mvc;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models.Request;

namespace RentCar.web.Controllers;

public class UserController : Controller
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
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return View();
        }
    }
}