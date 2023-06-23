namespace RentCar.web.Models;

public class LoginModel
{
    public string Token { get; set; }
    public DateTime FechaExp { get; set; }
    public int UserId { get; set; }
    public bool IsAdmin { get; set; }
}