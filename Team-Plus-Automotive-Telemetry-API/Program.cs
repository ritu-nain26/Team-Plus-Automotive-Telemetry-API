using Serilog;
using Serilog.Events;
using Team_Plus_Automotive_Telemetry_API.Handlers;
using Team_Plus_Automotive_Telemetry_API.Handlers.Data;
using Team_Plus_Automotive_Telemetry_API.Handlers.Login;
using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Feed;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Fetch;
using Team_Plus_Automotive_Telemetry_API.Models.Notify;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen only on HTTP and HTTPS
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5211); // HTTP
    serverOptions.ListenAnyIP(5210, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});

// Add services to the container.
// This registers services needed for both API controllers and MVC controllers with views
builder.Services.AddControllersWithViews();  // Use this instead of just AddControllers()

// Call the method to configure services
ConfigureServices(builder.Services);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Set the global minimum log level
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Ignore Microsoft logs below Warning level
    .MinimumLevel.Override("System", LogEventLevel.Warning)    // Ignore System logs below Warning level
    .WriteTo.File("logs/logfile.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// 3. Clear default logging providers and add Serilog
builder.Logging.ClearProviders(); // Clear other loggers
builder.Logging.AddSerilog();     // Add Serilog to the pipeline

var app = builder.Build();
app.UseHsts();
app.UseStaticFiles(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();

app.MapControllers();

// Configure routing
app.MapControllerRoute(
    name: "dashboard",
    pattern: "dashboard",
    defaults: new { controller = "Dashboard", action = "Index" }
);

// Map Default MVC Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Separate method to handle dependency injection
void ConfigureServices(IServiceCollection services)
{
    // Add controllers
    services.AddControllers();

    // Register your services here
    services.AddTransient<IFileHandler, FileHandler>();
    services.AddTransient<IHandler<NotifyRequest, string>, NotifyHandler>();
    services.AddTransient<IHandler<FeedDataRequest, FeedDataResponse>, FeedDataHandler>();
    services.AddTransient<IHandler<FetchDataRequest, FetchDataResponse>, FetchDataHandler>();

    // Add other necessary services
}
