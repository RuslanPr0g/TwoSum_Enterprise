using TwoSum.Application;
using TwoSum.Persistence;
using TwoSum.Quartz;
using TwoSum.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenseLayer(builder.Configuration);
builder.Services.AddJobs(builder.Configuration);
builder.Services.AddMessaging();

var app = builder.Build();

//app.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration))();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
