using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prueba.middleware
{
    //Creamos una clase para poder usar el middleware, en ella especificamos que es un middleware y que hereda de IApplicationBuilder
    public static class LogsResponseMiddlewareExtensions
    {
        public static void UseLogsResponse(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogsResponseMiddleware>();
        }
    }
    //Creamos la clase que hereda de IMiddleware
    public class LogsResponseMiddleware
    {
        private readonly ILogger<LogsResponseMiddleware> logger;
        private readonly RequestDelegate next;
        public LogsResponseMiddleware(ILogger<LogsResponseMiddleware> logger, RequestDelegate next)
        {
            this.next = next;

            this.logger = logger;
        }
        //Implementamos el metodo InvokeAsync para que se ejecute el middleware
        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var body = context.Response.Body;
                context.Response.Body = ms;

                await next(context);

                ms.Seek(0, SeekOrigin.Begin);
                string responseBody = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(body);
                context.Response.Body = body;
                logger.LogInformation(responseBody);
            }

        }
    }
}