namespace Cviko7ASPCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Services tady
            //builder.Services.AddSingleton();
            //builder.Services.AddScoped();
            //builder.Services.AddTransient();
            //Na poradi u services nezalezi
            builder.Services.AddSingleton<ErrorHandler>();
            builder.Services.AddSingleton<IMyLogger, TxtLogger>();
            builder.Services.AddScoped<DataService>();
            var app = builder.Build();
            // app.UseStaticFiles();
            //Zalezi na poradi
            app.UseMiddleware<ErrorMiddleware>();
            app.UseMiddleware<FileMiddleware>();
            app.UseMiddleware<AuthMiddleware>();
            app.UseMiddleware<FirstPageMiddleware>();
            app.UseMiddleware<FormMiddleware>();
            app.UseMiddleware<SecondPageMiddleware>();
            //app.MapGet("/", () => TypedResults.Content("<h1>Hello World!</h1>","text/html"));Nahrazeno FirstPageMiddlewarem
            /*app.MapGet("/second", () => new
            {
                a = 5
            });Nahrazen Second Page*/

            app.Run();
        }
    }
}
