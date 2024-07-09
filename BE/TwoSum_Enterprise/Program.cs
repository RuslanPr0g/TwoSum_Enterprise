using Microsoft.AspNetCore.Cors.Infrastructure;
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

#region  Setup CORS

var corsBuilder = new CorsPolicyBuilder();

//corsBuilder.AllowAnyOrigin(); // For anyone access.
corsBuilder.WithOrigins(["https://localhost:4002"]); // for a specific url. Don't add a forward slash on the end!
corsBuilder.AllowAnyHeader();
//corsBuilder.AllowCredentials();
corsBuilder.AllowAnyMethod();

builder.Services.AddCors(options =>
{
    options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
});

#endregion

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

app.UseCors("SiteCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<SolutionHub>("/realtimehub", map =>
{

});

app.MapControllers();

app.Run();
