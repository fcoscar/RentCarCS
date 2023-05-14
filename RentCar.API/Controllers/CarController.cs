using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentCar.domain.Entity;
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
        var cars = await this.carRepository.GetAll();
        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await this.carRepository.GetEntityById(id);
        return Ok(car);
    }
    [HttpGet("brand/{brand}")]
    public async Task<IActionResult> GetByBrand(string brand)
    {
        //var cars = await this.carRepository.Find(c => c.Marca == brand);
        var cars = await this.carRepository.GetCarsByBrand(brand);
        return Ok(cars);
    }
    [HttpGet("year/{year:int}")]
    public async Task<IActionResult> GetByYear(int year)
    {
        //var cars = await this.carRepository.Find(c => c.Year == year);
        var cars = await this.carRepository.GetCarsByYear(year);
        return Ok(cars);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Car car)
    {
        try
        {
            await this.carRepository.Save(car);
        }
        catch (Exception pex)
        {
            var mensaje = pex.Message;
        }
        return Ok();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await this.carRepository.Delete(id);
        return Ok();
    }
    
}