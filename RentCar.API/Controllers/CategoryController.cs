using Microsoft.AspNetCore.Mvc;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository categoryRepository;
    public CategoryController(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await this.categoryRepository.GetAll();
        return Ok(categories);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var categories = await this.categoryRepository.Find(c => c.Nombre == name);
        return Ok(categories);
    }

}