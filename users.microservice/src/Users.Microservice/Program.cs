using Common.MongoDB;
using Common.Settings;
using Microsoft.OpenApi.Models;
using Users.Microservice.Entities;

var builder = WebApplication.CreateBuilder(args);

var serviceSettings = builder
    .Configuration.GetSection(nameof(ServiceSettings))
    .Get<ServiceSettings>()!;

builder.Services.AddMongo().AddMongoRepository<User>("users");

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

app.UseAuthorization();

app.MapControllers(); // Agregar enrutamiento para controladores

app.Run();
