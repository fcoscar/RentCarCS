using Microsoft.AspNetCore.Mvc;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository userRepository;
    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await this.userRepository.GetAll();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        await this.userRepository.Save();
        return Ok();
    }
}