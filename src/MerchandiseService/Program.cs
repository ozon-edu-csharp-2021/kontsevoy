using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MerchandiseService;
using MerchandiseService.Infrastructure.Extensions;
using Serilog;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .AddSerilog()
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
        .AddInfrastructure()
        .AddHttp();