using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contract;
using RentCar.Application.Dtos.Car;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService carService;
    public CarController(ICarService carService)
    {
        this.carService = carService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cars = await this.carService.Get();
        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await this.carService.GetById(id);
        if (!car.Succes)
            return BadRequest(car);
        return Ok(car);
    }
    [HttpGet("brand/{brand}")]
    public async Task<IActionResult> GetByBrand(string brand)
    {
        //var cars = await this.carRepository.Find(c => c.Marca == brand);
        var cars = await this.carService.GetByBrand(brand);
        return Ok(cars);
    }
    [HttpGet("year/{year:int}")]
    public async Task<IActionResult> GetByYear(int year)
    {
        //var cars = await this.carRepository.Find(c => c.Year == year);
        var cars = await this.carService.GetByYear(year);
        return Ok(cars);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CarAddDto carAddDto)
    {
        var result = await this.carService.SaveCar(carAddDto);
        if (!result.Succes)
            return BadRequest(result);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CarUpdateDto carUpdateDto)
    {
        var result = await this.carService.ModifyCar(carUpdateDto);
        return Ok();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await this.carService.Delete(id);
        return Ok();
    }
    
}