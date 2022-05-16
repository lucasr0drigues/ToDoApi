using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Infra.Contexts;
using ToDoApi.Domain.Handlers;
using ToDo.Domain.Infra.Repositories;
using ToDoApi.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IToDoRepository, ToDoRepository>();
builder.Services.AddTransient<ToDoHandler, ToDoHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/...";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/...",
            ValidateAudience = true,
            ValidAudience = "...",
            ValidateLifetime = true
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); //
}

app.UseHttpsRedirection();

app.UseRouting(); //
app.UseCors(x => x     // basicamente permitir acessos localhost a essa aplicacao
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication(); // 
app.UseAuthorization();

// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// }));

app.MapControllers();

app.Run();
