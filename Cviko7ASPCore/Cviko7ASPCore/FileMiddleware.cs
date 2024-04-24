using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace Cviko7ASPCore
{
    public class FileMiddleware
    {
        private readonly RequestDelegate next;

        public FileMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext ctx)
        {
            string path = ctx.Request.Path;
            path = path.TrimStart('/');

            string dir = @"E:\Python Scripts\SKJ\client\images";
            path = Path.Combine(dir, path);
            if(File.Exists(path))
            {
                ctx.Response.ContentType = "image/jpeg";
                await ctx.Response.SendFileAsync(path);
            }
            else
            {
                await next(ctx);
            }
        }
    }
}
