using Microsoft.EntityFrameworkCore;
using Zadanie7.DAL;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<MasterContext>(opt =>
{
    opt.UseSqlServer("Server=localhost,1433;User Id=sa;Password=<your_password_here>;TrustServerCertificate=True");
    opt.LogTo(Console.WriteLine);
}
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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