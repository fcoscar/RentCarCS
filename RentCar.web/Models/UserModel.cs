namespace RentCar.web.Models;

public class UserModel
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string mail { get; set; }
    public string? docType { get; set; }
    public string? numDoc { get; set; }
    public bool isAdmin { get; set; }
    public List<CarModel> carros { get; set; }
    public List<AlquilerModel> alquileres { get; set; }
}