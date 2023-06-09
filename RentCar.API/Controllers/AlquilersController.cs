using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contract;
using RentCar.Application.Dtos.Alquiler;

namespace RentCar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        if (!alquileres.Succes)
        {
            return BadRequest(alquileres.Message);
        }
        return Ok(alquileres);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var alquiler = await alquilerService.GetById(id);
        if (!alquiler.Succes)
        {
            return BadRequest(alquiler.Message);
        }
        return Ok(alquiler);
    }

    [HttpPost("SaveAlquiler")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] AlquilerDto alquiler)
    {
        var result = await alquilerService.SaveAlquiler(alquiler);
        if (!result.Succes)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [HttpPost("UpdateAlquiler")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put([FromBody] AlquilerUpdateDto alquiler)
    {
        var result = await alquilerService.ModifyAlq(alquiler);
        if (!result.Succes)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}