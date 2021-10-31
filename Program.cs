var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();


app.UseMimeTypeValidation();
app.UseAcceptValidation();
app.MapControllers();

app.Run();
