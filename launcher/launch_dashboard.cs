
using Microsoft.Extensions.FileProviders;

static class launch_dashboard
{
    static public void execute_launch_dashboard(string[] args){
        var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    //todo: this will fail unless the current directory is launcher
    var reactAppPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "trading-dashboard", "build");

    //serve assets
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(reactAppPath),
        RequestPath = string.Empty,

    });

    //maps everything to index.html
    app.MapFallback(async context =>
    { 
        //todo: allow configuring for prod
        context.Response.Headers.Append("Access-Control-Allow-Origin", "*"); 
        context.Response.Headers.Append("Access-Control-Allow-Headers", "*");
        context.Response.Headers.Append("Access-Control-Allow-Methods", "*");
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(Path.Combine(reactAppPath, "index.html"));
    });

    //host krestrel web server
    app.Run();

    }
}