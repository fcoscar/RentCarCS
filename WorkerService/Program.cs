using RentCar.IOC.Dependencies;
using WorkerService;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;
        var connection = configuration.GetConnectionString("RentCarContext");

        services.AddContextDependecy(connection);
        services.AddRentCarDependency();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();