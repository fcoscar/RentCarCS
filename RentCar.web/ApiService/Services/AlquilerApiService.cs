using System.Text;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using RentCar.web.ApiService.Interfaces;
using RentCar.web.Models;
using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Services;

public class AlquilerApiService : IAlquilerApiService
{
    private readonly IHttpClientFactory clientFactory;
    private readonly ILogger<AlquilerApiService> logger;
    private readonly IConfiguration configuration;
    private readonly string baseUrl;

    public AlquilerApiService(IHttpClientFactory clientFactory, IConfiguration configuration,
        ILogger<AlquilerApiService> logger)
    {
        this.logger = logger;
        this.configuration = configuration;
        this.clientFactory = clientFactory;
        baseUrl = this.configuration["ApiConfig:urlBase"];
    }

    public async Task<AlquilerListResponse> GetAlquileres()
    {
        var alquilerList = new AlquilerListResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/Alquiler"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
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
        var alquiler = new AlquilerResponse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync($"{baseUrl}/Alquiler/{id}"))
                {
                    var resp = await response.Content.ReadAsStringAsync();
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

    public async Task<AlquilerAddRespoonse> SaveAlquiler(AlquilerAddResquest newAlquiler)
    {
        AlquilerAddRespoonse result = new AlquilerAddRespoonse();
        try
        {
            using (var httpClient = clientFactory.CreateClient())
            {
                StringContent request = new StringContent(JsonConvert.SerializeObject(newAlquiler), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"{baseUrl}/Alquiler/SaveAlquiler",request))
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AlquilerAddRespoonse>(resp);
                }
            }
        }
        catch (Exception e)
        {
            result.message = "Error guardando alquiler";
            result.succes = false;
            logger.Log(LogLevel.Error, $"{result.message}", e.ToString());
        }
        return result;
    }
}