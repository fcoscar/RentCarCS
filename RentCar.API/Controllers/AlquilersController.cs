using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contract;
using RentCar.Application.Dtos.Alquiler;

namespace RentCar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlquilerController : ControllerBase
{
    private readonly IAlquilerService alquilerService;

    public AlquilerController(IAlquilerService alquilerService)
    {
        this.alquilerService = alquilerService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var alquileres = await alquilerService.Get();
        return Ok(alquileres);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var alquiler = await alquilerService.GetById(id);
        return Ok(alquiler);
    }

    [HttpPost("SaveAlquiler")]
    public async Task<IActionResult> Post([FromBody] AlquilerDto alquiler)
    {
        var result = await alquilerService.SaveAlquiler(alquiler);
        if (!result.Succes)
        {
            return BadRequest();
        }
        return Ok(result);
    }
}