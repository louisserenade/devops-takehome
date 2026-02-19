using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace HWMonitor
{

    public class Worker : BackgroundService
    {
       private readonly ILogger<Worker> _logger;
       private readonly HttpClient _httpClient;
       private readonly string _logPath;

     public Worker(ILogger<Worker> logger)
     {
        _logger = logger;
        _httpClient = new HttpClient();
        _logPath = Path.Combine(AppContext.BaseDirectory, "service-log.txt");
     }

     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
     {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5001", stoppingToken);
                var message = $"{DateTime.Now} - Status: {(int)response.StatusCode} {response.ReasonPhrase}";
                
                await File.AppendAllTextAsync(_logPath, message + Environment.NewLine);

                if (!response.IsSuccessStatusCode)
                {
                    await File.AppendAllTextAsync(_logPath, "Service stopping due to non-200 response" + Environment.NewLine);
                    break;
                }
            }
            catch (Exception ex)
            {
                await File.AppendAllTextAsync(_logPath, $"{DateTime.Now} - ERROR: {ex.Message}" + Environment.NewLine);
                break;
            }

            await Task.Delay(60000, stoppingToken); // 60 seconds
        }
     }
    }
}
