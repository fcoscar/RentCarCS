using Microsoft.AspNetCore.Mvc;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models.Request;

namespace RentCar.web.Controllers;

public class AlquilerController : Controller
{
    private readonly IAlquilerApiService alquilerApiService;
    private readonly ICarApiService carApiService;

    public AlquilerController(IAlquilerApiService alquilerApiService, ICarApiService carApiService)
    {
        this.alquilerApiService = alquilerApiService;
        this.carApiService = carApiService;
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

    public async Task<ActionResult> Create(int id)
    {
        var car = await carApiService.GetCar(id);
        
        var alquiler = new AlquilerAddResquest()
        {
            From = DateTime.Now,
            To = DateTime.Now.AddDays(1),
            CarId = car.data.id,
            PricePerDay = car.data.pricePerDay,
            TotalPrice = car.data.pricePerDay
        };
        return View(alquiler);
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