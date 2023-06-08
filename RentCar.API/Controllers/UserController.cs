using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contract;
using RentCar.Application.Dtos.User;

namespace RentCar.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await this.userService.GetUsers();
        return Ok(users);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var users = await this.userService.GetById(id);
        return Ok(users);
    }

    [HttpPost("SaveUser")]
    public async Task<IActionResult> Post([FromBody] UserAddDto user)
    {
        await this.userService.SaveUser(user);
        return Ok();
    }
}