using System.Text;
using Newtonsoft.Json;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Services;

public class CarApiService : ICarApiService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly IConfiguration configuration;
    private readonly ILogger<CarApiService> logger;
    private readonly string baseUrl;
    public CarApiService(IHttpClientFactory clientFactory, 
        IConfiguration configuration,
        ILogger<CarApiService> logger)
    {
        this.clientFactory = clientFactory;
        this.configuration = configuration;
        this.logger = logger;
        this.baseUrl = this.configuration["ApiConfig:urlBase"]; //appsetting.json
    }
    public async Task<CarListResponse?> GetCars()
    {
        CarListResponse? carList = new CarListResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/Car"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string resp = await response.Content.ReadAsStringAsync();
                        carList = JsonConvert.DeserializeObject<CarListResponse>(resp);
                    }
                }
            }
        }
        catch (Exception e)
        {
            carList.message = "Error obteniendo los carros del api";
            carList.succes = false;
            logger.Log(LogLevel.Error, $"{carList.message}", e.ToString());
        }

        return carList;
    }

    public async Task<CarGetResponse> GetCar(int id)
    {

        CarGetResponse carGet = new CarGetResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/Car/{id}"))
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    carGet = JsonConvert.DeserializeObject<CarGetResponse>(resp);
                }
            }
        }
        catch (Exception e)
        {
            carGet.succes = false;
            carGet.message = "Error obteniendo el carro del api";
            logger.Log(LogLevel.Error, $"{carGet.message}", e.ToString());
        }

        return carGet;
    }

    public async Task<CarAddResponse> SaveCar(CarSaveRequest newCar)
    {
        CarAddResponse result = new CarAddResponse();
        try
        {
            using (var httpClient = this.clientFactory.CreateClient())
            {
                newCar.fecha = DateTime.Now;
                StringContent request = new StringContent(JsonConvert.SerializeObject(newCar), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{baseUrl}/Car/SaveCar", request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string resp = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<CarAddResponse>(resp);
                    }
                }
            }
        }
        catch (Exception e)
        {
            result.message = "Error gurdando carro";
            result.succes = false;
            logger.Log(LogLevel.Error, $"{result.message}", e.ToString());
        }
        return result;
    }

    public async Task<BaseResponse> UpdateCar(CarSaveRequest carToUpdate)
    {
        BaseResponse result = new BaseResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                //carToUpdate.fecha = DateTime.Now;
                StringContent request = new StringContent(JsonConvert.SerializeObject(carToUpdate), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{baseUrl}/Car/UpdateCar", request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string resp = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<CarAddResponse>(resp);
                    }
                }
            }
        }
        catch (Exception e)
        {
            result.message = "Error actualizando carro";
            result.succes = false;
            logger.Log(LogLevel.Error, $"{result.message}", e.ToString());
        }
        return result;
    }
}