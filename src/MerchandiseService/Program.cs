using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MerchandiseService;
using MerchandiseService.Infrastructure.Extensions;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
        .AddInfrastructure();