using ConsoleApp_Configuration_Reload;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
程式直接執行，一開始直接印出連線字串與ProjectId，在第二次執行DoSometing之前，
將修改的json，直接覆蓋 ConsoleApp_Configuration_Reload\ConsoleApp_Configuration_Reload\bin\Debug\net6.0\appsettings.json
再次印出的連線字串與ProjectId，只有連線字串是修改過的，ProjectId卻還是修改前的...
*/

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

//迴圈執行3次
for (int i = 0; i < 3; i++)
{
    myService.DoSometing();

    Thread.Sleep(8000);//暫停8秒
}

/*
修改的json:
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MyDbConnStr": "AAAAA"
  },
  "AppSettings": {
      "ProjectId": "BBBB"
  }
}
*/