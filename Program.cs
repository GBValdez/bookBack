using prueba;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var startUp = new startUp(builder.Configuration);
startUp.ConfigureServices(builder.Services);


var app = builder.Build();

startUp.Configure(app, builder.Environment);

app.Run();
