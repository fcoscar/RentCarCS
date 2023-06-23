using Microsoft.AspNetCore.Mvc;

namespace RentCar.web.Controllers;

public class BaseController : Controller
{
    public void SetSessionUser(string token, bool isAdmin, int userId)
    {
        HttpContext.Session.SetString("token", token);
        HttpContext.Session.SetInt32("userId", userId);
        HttpContext.Session.SetString("isAdmin", isAdmin ? "true" : "false");
    }

    public string GetToken()
    {
        return HttpContext.Session.GetString("token");
    }
    public int GetSessionUserId()
    {
        return (int)HttpContext.Session.GetInt32("userId");
    }
}