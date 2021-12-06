using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDiagnosticContext _diagnosticContext;

        public RequestLoggingMiddleware(RequestDelegate next, IDiagnosticContext diagnosticContext)
        {
            _next = next;
            _diagnosticContext = diagnosticContext;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            if (request.ContentType == "application/json" && (request.Method == "POST" || request.Method == "PUT"))
            {
                request.EnableBuffering();

                var buffer = new byte[Convert.ToInt32(request.ContentLength)];

                await request.Body.ReadAsync(buffer, 0, buffer.Length);

                var requestContent = Encoding.UTF8.GetString(buffer);

                _diagnosticContext.Set("RequestBody", requestContent);

                request.Body.Position = 0;
            }

            await _next(context);
        }
    }
}
