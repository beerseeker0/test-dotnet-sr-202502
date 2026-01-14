using ApplicantTracking.Api.Core.Extensions;
using ApplicantTracking.Application;
using ApplicantTracking.Application.Commands.Candidate.CreateCandidate;
using ApplicantTracking.Domain.Interfaces.IRepository;
using ApplicantTracking.Infrastructure;
using ApplicantTracking.Infrastructure.IRepository;
using ApplicantTracking.Infrastructure.Repository;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Candidates API",
        Version = "v1",
        Description = "API com CQRS + MediatR + FluentValidation"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ProjectPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:63278", "http://localhost:63279") // URLs permitidas
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCandidateCommand).Assembly)
);

builder.Services.AddValidatorsFromAssembly(
    typeof(CreateCandidateCommand).Assembly
);

builder.Services.AddValidatorsFromAssemblyContaining<CreateCandidateValidator>();

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>)
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IApplicationReadDbContext>(
    provider => provider.GetRequiredService<AppDbContext>()
);


var app = builder.Build();

app.UseCors("ProjectPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidates API v1");
        options.RoutePrefix = string.Empty; // /swagger
    });
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
