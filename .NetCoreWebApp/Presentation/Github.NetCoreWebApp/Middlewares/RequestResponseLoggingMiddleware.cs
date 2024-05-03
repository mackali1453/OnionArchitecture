using Github.NetCoreWebApp.Infrastructure.Common.Interfaces;
using System.Text;

namespace Github.NetCoreWebApp.Presentation.Middlewares
{
    public class RequestResponseLoggingMiddleware : IMiddleware
    {
        private readonly IServiceLogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(IServiceLogger<RequestResponseLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                string requestBody = await ReadRequestBody(context.Request);

                await _logger.Error($"Request Body: {requestBody}");

                var originalResponseBodyStream = context.Response.Body;
                using (var responseBodyStream = new MemoryStream())
                {
                    context.Response.Body = responseBodyStream;

                    await next(context);

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                    responseBodyStream.Seek(0, SeekOrigin.Begin);

                    await _logger.Error($"Response Body: {responseBody}");

                    await responseBodyStream.CopyToAsync(originalResponseBodyStream);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await _logger.Error($"Exception ex : {ex}");
            }
        }
        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                string requestBody = await reader.ReadToEndAsync();

                request.Body.Position = 0;

                return requestBody;
            }
        }
    }
}