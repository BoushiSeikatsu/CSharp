namespace Cviko7ASPCore
{
    public class SecondPageMiddleware
    {
        private readonly RequestDelegate next;
        public SecondPageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext ctx)
        {
            if (ctx.Request.Path == "/second")
            {
                throw new NotImplementedException();
                //ctx.Response.ContentType = "text/html";
                ctx.Response.Headers.ContentType = "text/html; charset=UTF-8";
                await ctx.Response.WriteAsync("<h1>Druhá stránka</h1>");
            }
        }
    }
}
