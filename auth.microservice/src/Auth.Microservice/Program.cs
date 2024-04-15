using Auth.Microservice.Clients;
using Auth.Microservice.Entities;
using Common.MongoDB;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Timeout;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo().AddMongoRepository<AuthUser>("auth");

builder
    .Services.AddHttpClient<UserClient>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5134");
    })
    .AddTransientHttpErrorPolicy(builderM =>
        builderM
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
    )
    .AddTransientHttpErrorPolicy(builderM =>
        builderM.Or<TimeoutRejectedException>().CircuitBreakerAsync(3, TimeSpan.FromSeconds(15))
    )
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthUsers Microservice API", Version = "v1" });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
