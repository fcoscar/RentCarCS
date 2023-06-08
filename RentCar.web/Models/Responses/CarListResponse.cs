namespace RentCar.web.Models.Responses;

public class CarListResponse : BaseResponse
{
    public List<CarModel>? data { get; set; }
}

public class CarGetResponse : BaseResponse
{
    public CarModel? data { get; set; }
}

public class CarAddResponse : BaseResponse
{
    public int Id { get; set; }
}