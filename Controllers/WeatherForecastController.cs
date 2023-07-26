using Hangfire;
using Hangfire.Client;
using Microsoft.AspNetCore.Mvc;

namespace HangFireDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine("dt" + DateTime.Now);
            // BackgroundJob.Enqueue(() => SendEmail("Hello Islam"));
            // BackgroundJob.Schedule(() => SendEmail("hello Islam"), TimeSpan.FromMinutes(1));
            RecurringJob.AddOrUpdate(() => SendEmail("hello Islam"), Cron.Minutely);
                //Cron.Montlyg(1) يوم واحد في الشهر
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [ApiExplorerSettings(IgnoreApi =true)]
        public void SendEmail(string email)
        {
            Console.WriteLine(email+":"+DateTime.Now);
        }
    }
}