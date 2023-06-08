namespace RentCar.web.Models;

public class CarModel
{
    public int id { get; set; }
    public string marca { get; set; }
    public string modelo { get; set; }
    public int year { get; set; }
    public int pasajeros { get; set; }
    public string descripcion { get; set; }
    public decimal pricePerDay { get; set; }
    public string categoria { get; set; }
    public int categoriaId { get; set; }
}