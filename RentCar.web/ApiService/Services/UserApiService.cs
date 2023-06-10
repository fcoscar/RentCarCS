using System.Text;
using Newtonsoft.Json;
using RentCar.Infraestructure.Models;
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

    public async Task<UserAddResponse> SaveUser(UserSaveRequest newUser)
    {
        UserAddResponse result = new UserAddResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                newUser.FechaCreacion = DateTime.Now;
                newUser.IsAdmin = false;
                StringContent request = new StringContent(JsonConvert.SerializeObject(newUser),Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{baseUrl}/User/SaveUser", request))
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
            result.message = "Error creando usuario";
            result.succes = false;
            logger.Log(LogLevel.Error, $"{result.message}", e.ToString());
        }
        return result;
    }
}