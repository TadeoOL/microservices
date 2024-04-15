using Common.MassTransit;
using Common.MongoDB;
using Common.Settings;
using MassTransit;
using Microsoft.OpenApi.Models;
using Users.Microservice.Entities;

var builder = WebApplication.CreateBuilder(args);

var serviceSettings = builder
    .Configuration.GetSection(nameof(ServiceSettings))
    .Get<ServiceSettings>()!;

builder.Services.AddMongo().AddMongoRepository<UserClass>("users").AddMassTransitWithRabbitMq();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users Microservice API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users Microservice API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
