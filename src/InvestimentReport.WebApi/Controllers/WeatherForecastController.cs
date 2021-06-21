using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestimentReport.Application.Interfaces.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InvestimentReport.WebApi.Controllers
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
        private readonly ICache cacheAdapter;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ICache cacheAdapter
        )
        {
            _logger = logger;
            this.cacheAdapter = cacheAdapter;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var resultData = await this.cacheAdapter
                .Obtain("WeatherForecast");

            if (!string.IsNullOrEmpty(resultData))
                return JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(resultData);

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            await this.cacheAdapter.Register(
                "WeatherForecast",
                JsonConvert.SerializeObject(result, Formatting.Indented),
                TimeSpan.Parse("0.00:10:00")
            );

            return result;
        }
    }
}
