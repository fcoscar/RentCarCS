namespace RentCar.web.Models.Request;

public class AlquilerAddResquest
{
    public int Id { get; set; }
    public int ReservationTime { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int CarId { get; set; }
    public DateTime Fecha { get; set; }
    public int? IdUsuarioCreacion { get; set; }
    public decimal PricePerDay { get; set; }
}