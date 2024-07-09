using TwoSum.Application;
using TwoSum.Messaging;
using TwoSum.Messaging.Hubs;
using TwoSum.Persistence;
using TwoSum.Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenseLayer(builder.Configuration);
builder.Services.AddJobs(builder.Configuration);
builder.Services.AddMessaging();

builder.Services.AddSignalR();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "*",
//                      policy =>
//                      {
//                          policy.WithOrigins("http://example.com");
//                          policy.WithMethods("GET", "POST");
//                          policy.AllowCredentials();
//                      });
//});

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

app.MapHub<SolutionHub>("/realtimehub", map =>
{

});

app.MapControllers();

app.Run();
