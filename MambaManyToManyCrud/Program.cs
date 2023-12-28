using FluentValidation.AspNetCore;
using Mamba.Business.Services.Implementations;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Repository.Interfaces;
using Mamba.Data.Repository.Implementations;
using MambaManyToManyCrud.DAL;
using MambaManyToManyCrud.DTOs.ProfessionDtos;
using MambaManyToManyCrud.MappingProfile;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(typeof(ProfessionCreateDtoValidator).Assembly);
});
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default2"));
});
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberProfessionRepository, MemberProfessionRepository>();
builder.Services.AddScoped<IProfessionRepository, ProfessionRepository>();
builder.Services.AddScoped<IProfessionService, ProfessionService>();

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
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
