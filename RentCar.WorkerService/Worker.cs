using RentCar.Application.Contract;
using RentCar.Application.Dtos.Alquiler;
using RentCar.Application.Dtos.Car;

namespace RentCar.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
            // change alqs status by date
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var alquilerService = scope.ServiceProvider.GetRequiredService<IAlquilerService>();
                var alqs = await alquilerService.Get();
                foreach (var alq in alqs.Data)
                {
                    if (DateTime.Now > alq.To)
                    {
                        await alquilerService.ModifyAlq(new AlquilerUpdateDto()
                        {
                            Id = alq.Id,
                            Status = "Terminado"
                        });
                    }
                    else if (DateTime.Now >= alq.From)
                    {
                        await alquilerService.ModifyAlq(new AlquilerUpdateDto()
                        {
                            Id = alq.Id,
                            Status = "Activo"
                        });
                    }
                }
                //set current car from and to with corresponding alq date
                using (var scope2 = serviceScopeFactory.CreateScope())
                {
                    var carService = scope2.ServiceProvider.GetRequiredService<ICarService>();
                    var cars = await carService.Get();

                    foreach (var car in cars.Data)
                    {
                        var carAlqs = car.Alqs;
                        foreach (var carAlq in carAlqs)
                        {
                            if (carAlq.Status == "Activo" && car.From != carAlq.From && car.To != carAlq.To)
                            {
                                await carService.ModifyCarFromTo(new CarUpdateFromTo()
                                {
                                    Id = car.Id,
                                    From = carAlq.From,
                                    To = carAlq.To,
                                });                                
                            }
                        }
                    }
                }
                await Task.Delay(2 * 43200000, stoppingToken);
                // cada 24 horas
            }
        }
    }
}