using Team_Plus_Automotive_Telemetry_API.Handlers.Login;
using Team_Plus_Automotive_Telemetry_API.Handlers;
using Team_Plus_Automotive_Telemetry_API.Models.Login;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Push;
using Team_Plus_Automotive_Telemetry_API.Handlers.Data;
using Team_Plus_Automotive_Telemetry_API.Infrastructure.File;
using Team_Plus_Automotive_Telemetry_API.Models.Data.Pull;

var builder = WebApplication.CreateBuilder(args);

// Call the method to configure services
ConfigureServices(builder.Services);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

// Separate method to handle dependency injection
void ConfigureServices(IServiceCollection services)
{
    // Add controllers
    services.AddControllers();

    // Register your services here
    services.AddTransient<IFileHandler, FileHandler>();
    services.AddTransient<IHandler<LoginRequest, LoginResponse>, LoginHandler>();
    services.AddTransient<IHandler<PushDataRequest, PushDataResponse>, PushDataHandler>();
    services.AddTransient<IHandler<PullDataRequest, PullDataResponse>, PullDataHandler>();

    // Add other necessary services
}
