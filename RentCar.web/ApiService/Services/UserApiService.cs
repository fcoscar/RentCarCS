using System.Text;
using Newtonsoft.Json;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Services;

public class UserApiService : IUserApiService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly IConfiguration configuration;
    private readonly ILogger<UserApiService> logger;
    private readonly string baseUrl;
    private readonly string authUrl;

    public UserApiService(IHttpClientFactory clientFactory,
        IConfiguration configuration,
        ILogger<UserApiService> logger)
    {
        this.clientFactory = clientFactory;
        this.configuration = configuration;
        this.logger = logger;
        baseUrl = this.configuration["ApiConfig:urlBase"];
        authUrl = this.configuration["ApiConfig:urlAuth"];
    }
    public async Task<UserListResponse> GetUsers()
    {
        UserListResponse userList = new UserListResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/User"))
                {
                    string resp = await response.Content.ReadAsStringAsync();
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
        UserResponse user = new UserResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/User/{id}"))
                {
                    string resp = await response.Content.ReadAsStringAsync();
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
        UserAddResponse result = new UserAddResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                userNew.IsAdmin = false;
                //userNew.FechaCreacion = DateTime.Now;
                StringContent request = new StringContent(JsonConvert.SerializeObject(userNew), Encoding.UTF8, "application/json");
                using(var response = await httpClient.PostAsync($"{authUrl}/CreateUser", request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string resp = await response.Content.ReadAsStringAsync();
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

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        LoginResponse result = new LoginResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                StringContent request = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{authUrl}/Login", request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string resp = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<LoginResponse>(resp);
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