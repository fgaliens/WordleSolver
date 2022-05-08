using SolverConsole.Extensions;
using WordleSolverWeb.WordleConfiguring;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.ConfigureWordleFileSource<Formatter, Filter>("LopatinVocab.txt");
services.ConfigureWordleServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await Task.Delay(2);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
