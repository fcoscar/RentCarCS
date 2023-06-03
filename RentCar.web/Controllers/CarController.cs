using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentCar.web.Models;
using RentCar.web.Models.Responses;

namespace RentCar.web.Controllers;

public class CarController : Controller
{
    private readonly ILogger<CarController> logger;
    private readonly IConfiguration configuration;
    private readonly HttpClientHandler clientHandler = new HttpClientHandler();
    public CarController(IConfiguration configuration, ILogger<CarController> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    public async Task<ActionResult> Index()
    {
        CarListResponse carList = new CarListResponse();
        try
        {
            using (var httpclient = new HttpClient(this.clientHandler))
            {
                var response = await httpclient.GetAsync("https://localhost:7198/api/Car");
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    carList = JsonConvert.DeserializeObject<CarListResponse>(resp);
                }
            }
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, e.ToString());
        }
        return View(carList.data);
    }

    public async Task<ActionResult> Details(int id)
    {
        CarGetResponse carGet = new CarGetResponse();
        try
        {
            using (var httpclient = new HttpClient(this.clientHandler))
            {
                var url = "https://localhost:7198/api/Car/" + id;
                var response = await httpclient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    carGet = JsonConvert.DeserializeObject<CarGetResponse>(resp);
                }
            }
        }
        catch (Exception e)
        {
            logger.Log(LogLevel.Error, e.ToString());
        }
        return View(carGet.data);
    }
    
    public async Task<ActionResult> Edit(int id)
    {
        return Ok();
    }
}