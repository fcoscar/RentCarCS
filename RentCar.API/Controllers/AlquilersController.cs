using Microsoft.AspNetCore.Mvc;
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

}
