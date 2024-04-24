namespace Cviko7ASPCore
{
    public class FirstPageMiddleware
    {
        private readonly RequestDelegate next;
        public FirstPageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext ctx, DataService dataService)
        {
            if(ctx.Request.Path == "/")
            {
                //ctx.Response.ContentType = "text/html";
                ctx.Response.Headers.ContentType = "text/html; charset=UTF-8";
                await ctx.Response.WriteAsync("<h1>Hello World!</h1><p>Žluťoučký</p>");
                await ctx.Response.WriteAsync(dataService.Msg);
            }
            else
            {
                await next(ctx);
            }
        }

    }
}
