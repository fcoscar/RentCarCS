namespace RentCar.web.Models;

public class UserModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Mail { get; set; }
    public string? DocType { get; set; }
    public string? NumDoc { get; set; }
    public bool IsAdmin { get; set; }
    public List<CarModel> Carros { get; set; }
}