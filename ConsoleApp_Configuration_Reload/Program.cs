using ConsoleApp_Configuration_Reload;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
                      .AddTransient<IConfiguration>(sp =>
                      {
                          return new ConfigurationBuilder()
                                 .SetBasePath(AppContext.BaseDirectory)
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .Build();
                      })
                      .AddTransient<IMyService, MyService>()
                      .AddTransient<AppSettings>(sp =>
                      {
                          var config = sp.GetRequiredService<IConfiguration>();
                          return config.GetSection(nameof(AppSettings)).Get<AppSettings>();
                      })
                      .BuildServiceProvider();

var myService = serviceProvider.GetRequiredService<IMyService>();

for (int i = 0; i < 3; i++)
{
    myService.DoSometing();

    Thread.Sleep(8000);
}