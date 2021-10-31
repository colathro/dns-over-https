var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<UdpClientService>();


var app = builder.Build();


app.UseMimeTypeValidation();
app.UseAcceptValidation();
app.MapControllers();

app.Run();
