namespace RentCar.web.Models.Request;

public class CarSaveRequest
{
    public int Id { get; set; }
    public int idUsuario { get; set; }
    public DateTime fecha { get; set; }
    public string marca { get; set; }
    public string modelo { get; set; }
    public int year { get; set; }
    public int pasajeros { get; set; }
    public string descripcion { get; set; }
    public decimal pricePerDay { get; set; }
    public int categoriaId { get; set; }
}