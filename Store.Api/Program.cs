
using System.Text.Json.Serialization;
using store.Store.Domain.StoreContext.Handlers;
using store.Store.Domain.StoreContext.Repositories;
using Store.Store.Domain.Services;
using Store.Store.Infra.StoreContext.DataContexts;
using Store.Store.Infra.StoreContext.Repositories;
using Store.Store.Infra.StoreContext.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureMvc();
ConfigureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
AppUses();
LoadConfiguration();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

void LoadConfiguration()
{
}

void ConfigureMvc()
{
    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
            options.SuppressModelStateInvalidFilter = true)
        .AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
}

void ConfigureServices()
{
    builder.Services.AddResponseCompression();
    builder.Services.AddScoped<StoreDataContext, StoreDataContext>();
    builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
    builder.Services.AddTransient<IEmailService, EmailService>();
    builder.Services.AddTransient<CustomerHandler, CustomerHandler>();

    // AddTransient - It aways create a new instance
    // AddScoped    - It aways create a new instance for a new request
    // AddSingleton - It creates one instance by App
}

void AppUses()
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseResponseCompression();
}
