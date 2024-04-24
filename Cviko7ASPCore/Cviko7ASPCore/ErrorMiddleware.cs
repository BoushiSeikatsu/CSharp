namespace Cviko7ASPCore
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext ctx, ErrorHandler handler)
        {
            try
            {
                await next(ctx);
            }
            catch (Exception ex)
            {
                await handler.Handler(ex);
            }
        }
    }
}
