using MediatR;
using MicroRabbitMq.Infra.IoC;
using MicroRabbitMq.MicroServices.Transfer.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbConnection<TransferDbContext>(builder.Configuration);
builder.Services.RegisterTransfer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
var app = builder.Build();
ApplyMigrations(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ApplyMigrations(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var bankingDbContext = scope.ApplyDbMigrations<TransferDbContext>();
        bankingDbContext.Database.Migrate();
    }
}
