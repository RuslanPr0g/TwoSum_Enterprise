using TwoSum.Application;
using TwoSum.Persistence;
using TwoSum.Quartz;
using TwoSum.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenseLayer(builder.Configuration);
builder.Services.AddJobs(builder.Configuration);
builder.Services.AddMessaging();

var app = builder.Build();

//app.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration))();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
