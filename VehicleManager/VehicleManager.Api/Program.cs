using Serilog;

var builder = WebApplication.CreateBuilder(args);


// ── Logging ──────────────────────────────────────────────────────────────────
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/vehiclemanager-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
// ── Services ──────────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "VehicleManager API",
        Version = "v1",
        Description = "API per la gestione veicoli, manutenzioni e scadenze"
    });
});

// AutoMapper: scansiona tutti i profili nel progetto Api
builder.Services.AddAutoMapper(typeof(Program));

// CORS: permette al client MAUI di chiamare l'API in locale
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalApp", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// TODO Step 3: aggiungere DbContext
// TODO Step 4: aggiungere Repository e UnitOfWork
// TODO Step 5: aggiungere i Services applicativi

var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleManager API v1");
        c.RoutePrefix = string.Empty; // Swagger alla root: http://localhost:5000
    });
}

app.UseSerilogRequestLogging();
app.UseCors("LocalApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();