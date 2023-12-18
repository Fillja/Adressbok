using Adressbok.Interfaces;
using Adressbok.ClassLibrary.Interfaces;
using Adressbok.Services;
using Adressbok.ClassLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


/// <summary>
/// Bygger Depenedency Injection med alla mina services och interface så jag enkelt kan kalla på dem.
/// </summary>
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{

    services.AddSingleton<IContactService, ContactService>();
    services.AddSingleton<IFileService, FileService>();
    services.AddSingleton<IListService, ListService>();
    services.AddSingleton<IMenuService, MenuService>();

}).Build();

builder.Start();

/// <summary>
/// Hämtar upp MenuService från interface. Rensar konsolen och startar huvudmenyn vid programstart.
/// </summary>
var service = builder.Services.GetRequiredService<IMenuService>();
Console.Clear();
service.ShowMainMenu();