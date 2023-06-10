namespace RentCar.web.Models;

public class AlquilerModel
{
    public int id { get; set; }
    public int reservationTime { get; set; }
    public double totalPrice { get; set; }
    public DateTime from { get; set; }
    public DateTime to { get; set; }
    public int car { get; set; }
}