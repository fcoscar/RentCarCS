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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await this.categoryRepository.GetEntityById(id);
        return Ok(category);
    }
    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var categories = await this.categoryRepository.GetCategoryByName(name);
        Console.WriteLine(categories);
        return Ok(categories);
    }
    [HttpPost("SaveCategory")]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
        await categoryRepository.Save(category);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await categoryRepository.Delete(id);
        return Ok();
    }
}