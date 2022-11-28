using Microsoft.EntityFrameworkCore;
using Revision_API.API.Messaging;
using Revision_API.API.Messaging.Interfaces;
using Revision_API.Data;
using Revision_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Revision_APIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Revision_APIContext") 
                         ?? throw new InvalidOperationException("Connection string 'Revision_APIContext' not found."))
        .UseLazyLoadingProxies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessagingApi, MessagingApi>();
builder.Services.AddHostedService<ReminderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    var port = Environment.GetEnvironmentVariable("PORT");
    app.Urls.Add($"http://*:{port}");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
