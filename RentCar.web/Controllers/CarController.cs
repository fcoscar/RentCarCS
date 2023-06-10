using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models;
using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.Controllers;

public class CarController : Controller
{
    private readonly ILogger<CarController> logger;
    private readonly ICarApiService carApiService;

    public CarController(IConfiguration configuration, ILogger<CarController> logger,
        ICarApiService carApiService)
    {
        this.logger = logger;
        this.carApiService = carApiService;
    }

    public async Task<ActionResult> Index()
    {
        // CarListResponse carList = new CarListResponse();
        // try
        // {
        //     using (var httpclient = new HttpClient(this.clientHandler))
        //     {
        //         var response = await httpclient.GetAsync("https://localhost:7198/api/Car");
        //         if (response.IsSuccessStatusCode)
        //         {
        //             string resp = await response.Content.ReadAsStringAsync();
        //             carList = JsonConvert.DeserializeObject<CarListResponse>(resp);
        //         }
        //     }
        // }
        // catch (Exception e)
        // {
        //     logger.Log(LogLevel.Error, e.ToString());
        // }
        var resp = await carApiService.GetCars();
        return View(resp.data);
    }

    public async Task<ActionResult> Details(int id)
    {
        // CarGetResponse carGet = new CarGetResponse();
        // try
        // {
        //     using (var httpclient = new HttpClient(this.clientHandler))
        //     {
        //         var url = "https://localhost:7198/api/Car/" + id;
        //         var response = await httpclient.GetAsync(url);
        //         if (response.IsSuccessStatusCode)
        //         {
        //             string resp = await response.Content.ReadAsStringAsync();
        //             carGet = JsonConvert.DeserializeObject<CarGetResponse>(resp);
        //         }
        //     }
        // }
        // catch (Exception e)
        // {
        //     logger.Log(LogLevel.Error, e.ToString());
        // }
        var resp = await carApiService.GetCar(id);
        return View(resp.data);
    }

    public async Task<ActionResult> Update(int id)
    {
        var car = await carApiService.GetCar(id);

        var carToUpdate = new CarSaveRequest()
        {
            marca = car.data.marca,
            modelo = car.data.modelo,
            year = car.data.year,
            pasajeros = car.data.pasajeros,
            descripcion = car.data.descripcion,
            pricePerDay = car.data.pricePerDay,
            categoriaId = (Categories) car.data.categoriaId
        };

        return View(carToUpdate);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Update(CarSaveRequest carToUpdate)
    {
        try
        {
            var resp = await carApiService.UpdateCar(carToUpdate);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }


    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CarSaveRequest newCar)
    {
        try
        {
            var resp = await carApiService.SaveCar(newCar);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return View();
        }
    }
}