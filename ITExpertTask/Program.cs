using ITExpertTask.Data;
using ITExpertTask.Middleware;
using ITExpertTask.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.IgnoreNullValues = true;
}); ;
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(c => c.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ITExpertTask;Trusted_Connection=True;"));
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IDataContextProvider>(x => new DataContextProvider(@"Server=(localdb)\mssqllocaldb;Database=ITExpertTask;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();


app.UseAuthorization();



app.MapControllers();

app.Run();
