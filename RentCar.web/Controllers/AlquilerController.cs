using Microsoft.AspNetCore.Mvc;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models.Request;

namespace RentCar.web.Controllers;

public class AlquilerController : Controller
{
    private readonly IAlquilerApiService alquilerApiService;

    public AlquilerController(IAlquilerApiService alquilerApiService)
    {
        this.alquilerApiService = alquilerApiService;
    }

    public async Task<ActionResult> Index()
    {
        var resp = await alquilerApiService.GetAlquileres();
        return View(resp.data);
    }

    public async Task<ActionResult> Details(int id)
    {
        var alquiler = await alquilerApiService.GetAlquiler(id);
        return View(alquiler.data);
    }

    public ActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(AlquilerAddResquest newAlquiler)
    {
        try
        {
            var resp = alquilerApiService.SaveAlquiler(newAlquiler);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return View();
        }
    }
}