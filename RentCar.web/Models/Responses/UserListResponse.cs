namespace RentCar.web.Models.Responses;

public class UserListResponse : BaseResponse
{
    public List<UserModel> data { get; set; }
}

public class UserAddResponse : BaseResponse
{
    public int Id { get; set; }
}