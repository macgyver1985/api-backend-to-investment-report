using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestimentReport.Application.Interfaces.Adapters;
using InvestimentReport.CrossCutting.Trace.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ILogger loggerAdapter;
        private readonly ICache cacheAdapter;

        public WeatherForecastController(
            ILogger loggerAdapter,
            ICache cacheAdapter
        )
        {
            this.loggerAdapter = loggerAdapter;
            this.cacheAdapter = cacheAdapter;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var resultData = await this.cacheAdapter
                .Obtain("WeatherForecast");

            if (!string.IsNullOrEmpty(resultData))
            {
                var resultCache = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(resultData);

                await this.loggerAdapter
                    .Debug<WeatherForecastController, IEnumerable<WeatherForecast>>(
                        Guid.NewGuid(),
                        "Dados obtidos do cache",
                        resultCache
                    );

                return resultCache;
            }

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
