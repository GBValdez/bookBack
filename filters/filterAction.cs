using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace prueba.filters
{
    public class MyFilterAction : IActionFilter
    {
        private ILogger<MyFilterAction> logger { get; }
        public MyFilterAction(ILogger<MyFilterAction> logger)
        {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.logger.LogInformation("Saliendo");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.logger.LogInformation("Entrando");
        }
    }
}