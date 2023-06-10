using System.Text;
using Newtonsoft.Json;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;
using UserModel = RentCar.web.Models.UserModel;

namespace RentCar.web.ApiService.Services;

public class UserApiService : IUserApiService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly IConfiguration configuration;
    private readonly ILogger<UserApiService> logger;
    private readonly string baseUrl;

    public UserApiService(IHttpClientFactory clientFactory,
        IConfiguration configuration,
        ILogger<UserApiService> logger)
    {
        this.clientFactory = clientFactory;
        this.configuration = configuration;
        this.logger = logger;
        baseUrl = this.configuration["ApiConfig:urlBase"];
    }

    public async Task<UserListResponse> GetUsers()
    {
        var userList = new UserListResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/User"))
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<UserListResponse>(resp);
                }
            }
        }
        catch (Exception e)
        {
            userList.succes = false;
            userList.message = "Error obteniendo usuarios";
            logger.Log(LogLevel.Error, $"{userList.message}", e.ToString());
        }

        return userList;
    }

    public async Task<UserResponse> GetUser(int id)
    {
        var user = new UserResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/User/{id}"))
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserResponse>(resp);
                }
            }
        }
        catch (Exception e)
        {
            user.message = "Error obteniendo usuario por id";
            user.succes = false;
            logger.Log(LogLevel.Error, $"{user.message}", e.ToString());
        }

        return user;
    }

    public async Task<UserAddResponse> SaveUser(UserSaveRequest userNew)
    {
        var result = new UserAddResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                userNew.IsAdmin = false;
                var request = new StringContent(JsonConvert.SerializeObject(userNew), Encoding.UTF8,
                    "application/json");
                using (var response = await httpClient.PostAsync($"{baseUrl}/User/SaveUser", request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<UserAddResponse>(resp);
                    }
                }
            }
        }
        catch (Exception e)
        {
            result.message = "Error guardando usuario";
            result.succes = false;
            logger.Log(LogLevel.Error, $"{result.message}", e.ToString());
        }

        return result;
    }
}