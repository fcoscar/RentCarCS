using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contract;
using RentCar.Application.Dtos.User;
using RentCar.Application.Models;
using RentCar.Infraestructure.Models;
using RentCat.Auth.Api.Core;

namespace RentCat.Auth.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowAll")]
public class AuthController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IConfiguration configuration;

    public AuthController(IUserService userService,
        IConfiguration configuration)
    {
        this.userService = userService;
        this.configuration = configuration;
    }
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserAddDto user)
    {
        var result = await userService.SaveUser(user);
        return Ok(result);
    }
    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn([FromBody] GetUserInfo userInfo)
    {
        var result = await userService.GetUserLogInfo(userInfo);
        if (result.Succes)
        {
            UserModel user = result.Data;
            TokenInfo tokenInfo = TokenHelper.GetToken(user, configuration["TokenInfo:SigningKey"]);
            result.Data = tokenInfo;
        }
        else
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
}