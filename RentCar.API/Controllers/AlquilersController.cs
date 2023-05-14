using Microsoft.AspNetCore.Mvc;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AlquilerController : ControllerBase
{
    private readonly IAlquilerRepository alquilerRepository;
    public AlquilerController(IAlquilerRepository alquilerRepository)
    {
        this.alquilerRepository = alquilerRepository;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var alquileres = await this.alquilerRepository.GetAll();
        return Ok(alquileres);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var alquiler = await this.alquilerRepository.GetEntityById(id);
        return Ok(alquiler);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Alquiler alquiler)
    {
        await this.alquilerRepository.Save(alquiler);
        return Ok();
    }

}
