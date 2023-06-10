using Microsoft.AspNetCore.Mvc;
using RentCar.web.ApiService.Interfaces;

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
}