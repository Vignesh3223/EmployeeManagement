using EmployeeManagement.Business;
using EmployeeManagement.Persistence.IRepository;
using EmployeeManagement.Persistence.Models;
using EmployeeManagement.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmployeeManagementContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("coreConnection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "JWTAuthenticationServer",
        ValidAudience = "JWTServicePostmanClient",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs05Dsa"))
    };
});
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "AllowAccess",
      builder =>
      {
        builder.WithOrigins("http://localhost:4200", "http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
      });
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(EmployeeManager));
builder.Services.AddScoped(typeof(DepartmentManager));
builder.Services.AddScoped(typeof(DesignationManager));
builder.Services.AddScoped(typeof(SalaryManager));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAccess");
app.UseAuthentication();

app.UseAuthorization();



app.MapControllers();

app.Run();
