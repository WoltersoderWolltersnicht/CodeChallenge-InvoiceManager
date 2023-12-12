using InvoiceManager.Infrastructure.EF;
using InvoiceManager.Infrastructure.EF.MySql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseMySqlDatabase(builder.Configuration);
builder.Services.UseMySqlRepositories();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<InvoiceManagerDbContext>();
var isCreated = dbContext.Database.EnsureCreated();
if (!isCreated) throw new Exception("Database not created succesfully");

app.Run();