using CestNcm.DataImporter.Loaders;
using CestNcm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var configuration = builder.Build();

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

services.AddTransient<JsonImporter>();

var serviceProvider = services.BuildServiceProvider();
var importer = serviceProvider.GetRequiredService<JsonImporter>();

var filePath = Path.Combine(AppContext.BaseDirectory, "dados_cest.json");
await importer.ImportFromFileAsync(filePath);

