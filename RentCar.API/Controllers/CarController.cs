using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarRepository carRepository;
    public CarController(ICarRepository carRepository)
    {
        this.carRepository = carRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cars =  await this.carRepository.GetAll();
        return Ok(cars);
    }

    [HttpGet("{brand}")]
    public async Task<IActionResult> GetByBrand(string brand)
    {
        var cars = await this.carRepository.GetCarsByBrand(brand);
        return Ok(cars);
    }
    [HttpGet("{year:int}")]
    public async Task<IActionResult> GetByYear(int year)
    {
        var cars = await this.carRepository.GetCarsByYear(year);
        return Ok(cars);
    }
}