using Microsoft.Extensions.Configuration;

namespace ConsoleApp_Configuration_Reload
{
    public class MyService : IMyService
    {
        private readonly IConfiguration configuration;
        private readonly AppSettings appSettings;

        public MyService(IConfiguration configuration, AppSettings appSettings)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }
        public void DoSometing()
        {
            Console.WriteLine(this.configuration.GetConnectionString("MyDbConnStr"));

            Console.WriteLine(this.appSettings.ProjectId);
        }
    }
}
