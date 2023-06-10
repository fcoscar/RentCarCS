using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Services;

public class AlquilerApiService : IAlquilerApiService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly ILogger<AlquilerApiService> logger;
    private readonly IConfiguration configuration;
    private readonly string baseUrl;
    
    public AlquilerApiService(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<AlquilerApiService> logger)
    {
        this.logger = logger;
        this.configuration = configuration;
        this.clientFactory = clientFactory;
        baseUrl = this.configuration["ApiConfig:urlBase"];
    }
    
    public async Task<AlquilerListResponse> GetAlquileres()
    {
        AlquilerListResponse alquilerList = new AlquilerListResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/Alquiler"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string resp = await response.Content.ReadAsStringAsync();
                        alquilerList = JsonConvert.DeserializeObject<AlquilerListResponse>(resp);
                    }
                }
            }
        }
        catch (Exception e)
        {
            alquilerList.message = "Error obteniendo alquileres";
            alquilerList.succes = false;
            logger.Log(LogLevel.Error, $"{alquilerList.message}", e.ToString());
        }

        return alquilerList;
    }

    public async Task<AlquilerResponse> GetAlquiler(int id)
    {
        AlquilerResponse alquiler = new AlquilerResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/Alquiler/{id}"))
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    alquiler = JsonConvert.DeserializeObject<AlquilerResponse>(resp);    
                }
            }
        }
        catch (Exception e)
        {
            alquiler.message = "Error obteniendo alquileres";
            alquiler.succes = false;
            logger.Log(LogLevel.Error, $"{alquiler.message}", e.ToString());
        }
        return alquiler;
    }
}