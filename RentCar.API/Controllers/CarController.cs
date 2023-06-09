using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contract;
using RentCar.Application.Dtos.Car;

namespace RentCar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowAll")]

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
        var cars = await carService.Get();
        if (!cars.Succes)
        {
            return BadRequest(cars.Message);
        }
        return Ok(cars);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await carService.GetById(id);
        if (!car.Succes)
            return BadRequest(car);
        return Ok(car);
    }

    [HttpGet("category/{category:int}")]
    public async Task<IActionResult> GetByBrand(int category)
    {
        //var cars = await this.carRepository.Find(c => c.Marca == brand);
        var cars = await carService.GetByCategory(category);
        if (!cars.Succes)
        {
            return BadRequest(cars.Message);
        }
        return Ok(cars);
    }

    [HttpGet("brand/{brand}")]
    public async Task<IActionResult> GetByBrand(string brand)
    {
        //var cars = await this.carRepository.Find(c => c.Marca == brand);
        var cars = await carService.GetByBrand(brand);
        if (!cars.Succes)
        {
            return BadRequest(cars.Message);
        }
        return Ok(cars);
    }

    [HttpGet("year/{year:int}")]
    public async Task<IActionResult> GetByYear(int year)
    {
        //var cars = await this.carRepository.Find(c => c.Year == year);
        var cars = await carService.GetByYear(year);
        if (!cars.Succes)
        {
            return BadRequest(cars.Message);
        }
        return Ok(cars);
    }

    [HttpGet("year/{from:int}/{to:int}")]
    public async Task<IActionResult> GetByYearRange(int from, int to)
    {
        var cars = await carService.GetByYearRange(from, to);
        if (!cars.Succes)
        {
            return BadRequest(cars.Message);
        }
        return Ok(cars);
    }

    [HttpPost("SaveCar")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] CarAddDto carAddDto)
    {
        var result = await carService.SaveCar(carAddDto);
        if (!result.Succes)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("UpdateCar")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put([FromBody] CarUpdateDto carUpdateDto)
    {
        var result = await carService.ModifyCar(carUpdateDto);
        if (!result.Succes)
            return BadRequest(result);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(int id)
    {
        await carService.Delete(id);
        return Ok();
    }
}