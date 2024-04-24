using System.Net;

namespace Cviko7ASPCore
{
    public class FormMiddleware
    {
        private readonly RequestDelegate next;

        public FormMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async void Invoke(HttpContext ctx)
        {
            if(ctx.Request.Path == "/form")
            {
                if(ctx.Request.Method == "POST")
                {
                    string name = ctx.Request.Form["jmeno"];
                    ctx.Response.ContentType = "text/html charset=UTF-8";
                    await ctx.Response.WriteAsync("odesláno - " + WebUtility.HtmlEncode(name));
                    return;
                }
                ctx.Response.Headers.ContentType = "text/html charset=UTF-8";
                await ctx.Response.WriteAsync(@"
                    <form method=""post"">
                        <input name=""jmeno"" />
                        <button type=""submit"">odeslat</button>
                    </form>
                ");
            }
            else
            {
                await next(ctx);
            }
        }
    }
}
