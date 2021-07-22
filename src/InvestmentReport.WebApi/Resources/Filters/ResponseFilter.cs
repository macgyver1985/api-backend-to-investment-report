using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InvestmentReport.WebApi.Resources.Filters
{

    /// <summary>
    /// Filtro que padroniza as respostas de todas as actions.
    /// </summary>
    internal class ResponseFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                throw context.Exception;

            var result = context.Result as ObjectResult;

            result.StatusCode = (int)result.Value.GetType().GetProperty("StatusCode").GetValue(result.Value);
        }

    }

}