namespace Cviko7ASPCore
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext ctx, DataService dataService)
        {
            string ua = ctx.Request.Headers.UserAgent;
            dataService.Msg = ua;
            if (ua.Contains("Chrome") && !ua.Contains("Edg")) 
            {
                await next(ctx);
            }
            else
            {
                ctx.Response.Headers.ContentType = "text/html; charset=UTF-8";
                ctx.Response.StatusCode = 403;
                await ctx.Response.WriteAsync("Přístup odepřen");
            }
        }
    }
}
