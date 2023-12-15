using InvoiceManager.Application.Handler;
using InvoiceManager.Infrastructure.EF;
using InvoiceManager.Infrastructure.EF.MySql;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseMySqlDatabase(builder.Configuration);
builder.Services.AddRepositories();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(BaseHandler).Assembly));

var app = builder.Build();
app.MapControllers();

app.Services.EnsureEFConnection();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();