namespace RentCar.web.Models.Responses;

public class UserListResponse : BaseResponse
{
    public List<UserModel> data { get; set; }
}

public class UserResponse : BaseResponse
{
    public UserModel data { get; set; }
}

public class UserAddResponse : BaseResponse
{
    
}

public class LoginResponse : BaseResponse
{
    public LoginModel data { get; set; }
}