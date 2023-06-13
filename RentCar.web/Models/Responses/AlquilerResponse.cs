namespace RentCar.web.Models.Responses;

public class AlquilerListResponse : BaseResponse
{
    public List<AlquilerModel> data { get; set; }
}

public class AlquilerResponse : BaseResponse
{
    public AlquilerModel data { get; set; }
}

public class AlquilerAddRespoonse : BaseResponse
{
    public int Id { get; set; }
}