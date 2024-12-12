using Test_Smart.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.ConfigureServices();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();
