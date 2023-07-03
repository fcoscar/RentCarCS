using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contract;
using RentCar.Application.Dtos.User;
using RentCar.Application.Models;

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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Get()
    {
        var users = await userService.GetUsers();
        if (!users.Succes)
        {
            return BadRequest(users.Message);
        }
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var users = await userService.GetById(id);
        if (!users.Succes)
        {
            return BadRequest(users.Message);
        }
        return Ok(users);
    }

    /*[HttpPost("LogIn")]
    public async Task<IActionResult> LogIn([FromBody] GetUserInfo userInfo)
    {
        var user = await userService.GetUserLogInfo(userInfo);
        return Ok(user);
    }

    [HttpPost("SaveUser")]
    public async Task<IActionResult> Post([FromBody] UserAddDto user)
    {
        await userService.SaveUser(user);
        return Ok();
    }*/
}