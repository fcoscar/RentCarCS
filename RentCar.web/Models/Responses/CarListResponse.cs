namespace RentCar.web.Models.Responses;

public class CarListResponse
{
    public object? message { get; set; }
    public bool succes { get; set; }
    public List<CarModel>? data { get; set; }
}

public class CarGetResponse
{
    public object? message { get; set; }
    public bool succes { get; set; }
    public CarModel? data { get; set; }
}