using InvoiceManager.Application.Handler;
using InvoiceManager.Infrastructure.EF;
using InvoiceManager.Infrastructure.EF.MySql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseMySqlDatabase(builder.Configuration);
builder.Services.UseMySqlRepositories();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(BaseHandler).Assembly));

var app = builder.Build();

app.MapControllers();

//var scope = app.Services.CreateScope();
//var dbContext = scope.ServiceProvider.GetService<InvoiceManagerDbContext>();
//var isCreated = dbContext.Database.EnsureCreated();
//if (!isCreated) throw new Exception("Database not created succesfully");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();