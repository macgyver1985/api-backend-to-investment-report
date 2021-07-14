using System.Linq;
using System.Net;
using System.Threading.Tasks;
using InvestmentReport.WebApi.Resources.HealthCheck.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace InvestmentReport.WebApi.Resources.HealthCheck.UI
{

    /// <summary>
    /// Classe que auxília na apresentação dos relatório de verificação de saúde dos serviços configurados.
    /// </summary>
    public static class HealthCheckResponse
    {

        /// <summary>
        /// Método que converte os dados do HealthReport para um HealthDTO.
        /// </summary>
        /// <param name="report">Recebe instância de um HealthReport.</param>
        /// <returns>Retorna instância de um HealthDTO.</returns>
        /// <see cref="HealthDTO"/>
        /// <see cref="HealthReport"/>
        private static HealthDTO ConvertToDTO(HealthReport report)
        {
            HealthDTO result = new HealthDTO()
            {
                Status = report.Status,
                TotalDuration = report.TotalDuration
            };

            report.Entries.ToList().ForEach(t =>
            {
                var item = new HealthItemDTO()
                {
                    ServiceName = t.Key,
                    Duration = t.Value.Duration,
                    Status = t.Value.Status,
                    Tags = t.Value.Tags
                };

                t.Value.Data?.ToList().ForEach(d => item.Data.Add(d.Key, d.Value));

                if (t.Value.Exception != null)
                    item.Exception = new HealthExceptionDTO()
                    {
                        Message = t.Value.Exception.Message,
                        Source = t.Value.Exception.Source,
                        StackTrace = t.Value.Exception.StackTrace,
                        Type = t.Value.Exception.GetType().Name
                    };

                result.HealthItems.Add(item);
            });

            return result;
        }

        /// <summary>
        /// Método assíncrono que retorna os dados do relatório de saúde em Json.
        /// </summary>
        /// <param name="context">Instância de um HttpContext.</param>
        /// <param name="report">Instância de um HealthReport.</param>
        /// <returns>Retorna instância de uma Task.</returns>
        /// <remarks>
        /// Deve ser usado nas opções de configuração do middleware de verificação de saúde conforme exemplo abaixo:
        /// <code>
        /// app.UseHealthChecks("/hc", new HealthCheckOptions()
        /// {
        ///     Predicate = _ => true,
        ///     ResponseWriter = HealthCheckResponse.Json
        /// });
        /// </code>
        /// </remarks>
        public static async Task Json(HttpContext context, HealthReport report)
        {
            var result = ConvertToDTO(report);

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
        }

    }

}