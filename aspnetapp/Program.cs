using System.Text.Json.Serialization;
//using ClassLibrary1;

//var class1 = new Class1();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

IConfigurationSection section = builder.Configuration.GetSection("ConnectionStrings");
ConnectionStringSettings settings = new();
section.Bind(settings);

Console.WriteLine("Connection string: " + settings.DefaultConnection);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHealthChecks();

// Uncomment if using System.Text.Json source generation
// builder.Services.ConfigureHttpJsonOptions(options =>
// {
//     options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
// });

var app = builder.Build();

app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

CancellationTokenSource cancellation = new();
app.Lifetime.ApplicationStopping.Register( () =>
{
    cancellation.Cancel();
});

app.MapGet("/Environment", () =>
{
    return new EnvironmentInfo();
});

// This API demonstrates how to use task cancellation
// to support graceful container shutdown via SIGTERM.
// The method itself is an example and not useful.
app.MapGet("/Delay/{value}", async (int value) =>
{
    try
    {
        await Task.Delay(value, cancellation.Token);
    }
    catch(TaskCanceledException)
    {
    }

    return new Operation(value);
});

app.Run();

Console.WriteLine("Hellokes there, I'm a .NET app from the future!");

[JsonSerializable(typeof(EnvironmentInfo))]
[JsonSerializable(typeof(Operation))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

public record struct Operation(int Delay);

public class ConnectionStringSettings
{
    public string DefaultConnection { get; set; }
}

