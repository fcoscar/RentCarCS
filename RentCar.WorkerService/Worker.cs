using RentCar.Application.Contract;
using RentCar.Application.Dtos.Alquiler;
using RentCar.Application.Services;

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
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var myService = scope.ServiceProvider.GetRequiredService<IAlquilerService>();
                var alqs = await myService.Get();
                foreach (var alq in alqs.Data)
                {
                    if (DateTime.Now > alq.To)
                    {
                        await myService.ModifyAlq(new AlquilerUpdateDto()
                        {
                            Id = alq.Id,
                            Status = "Terminado"
                        });
                    }
                }
            }
            await Task.Delay(2000, stoppingToken);
        }
    }
}